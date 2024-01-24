using System; using System.Collections;
using InvOp = System.InvalidOperationException;
using Doc = System.Xml.XmlDocument;
using Elem = System.Xml.XmlElement; using Node = System.Xml.XmlNode;
using static UnityEngine.Debug;

namespace Activ.XML{
public static class XmlReader{

    const string TYPE = "t";

    public static object Read<T>(string arg) => Read(arg, typeof(T));

    public static object Read(string arg, Type type=null){
        if(arg == null) return null;
        var doc = new Doc();
        doc.LoadXml(arg);
        return Read(doc.DocumentElement, type);
    }

    static object Read(Node node, Type type){
        string S = ReadTypeName(node as Elem);
        string T = type?.Name(out int gcount);
        if(!MatchTypeNames(S, T)) throw new InvOp(
            $"Source type [{S}] (XML) does not match "
          + $"target type [{T}] (C#)"
        );
        var text = node.InnerText;
        if(type?.IsEnum ?? false){
            return Enum.Parse(type, text);
        }
        var primitive = PrimitiveType.FromString(S, text);
        if(primitive != null){
            return primitive;
        }
        if(S == "String"){
            return node.InnerText;
        }
        return ReadObject(node, type);
    }

    static bool MatchTypeNames(string source, string target){
        if(source == null) return true;
        if(target == null) return true;
        target = target.Replace("[]", "-Array");
        return source == target;
    }

    static object ReadObject(Node node, Type S){
        var obj = Instantiate(node, S, out Type T);
        int count = node.ChildNodes.Count;
        for(var i = 0; i < count; i++){
            var child = node.ChildNodes[i];
            var elem      = child as Elem;
            var fieldName = ReadFieldName(elem);
            if(string.IsNullOrEmpty(fieldName)){
                var value = Read(elem, null);
                AddChild(obj, value, i);
            }else{
                var fieldType = T.FieldType(fieldName);
                if(fieldType == null){
                    throw new InvOp($"?field {S.Name}/{fieldName}");
                }
                var value = Read(elem, fieldType);
                var field = T.GetField(fieldName);
                field.SetValue(obj, value);
            }
        }
        return obj;
    }

    static void AddChild(dynamic parent, dynamic value,  int index){
        if(parent.GetType().IsArray){
            parent[index] = value;
        }else{
            try{
                parent.Add (value);
            }catch(Exception){
                UnityEngine.Debug.LogError($"Cannot add {value} of type ({value.GetType()}) to {parent}");
                throw;
            }
        }
    }

    static object Instantiate(Node node, Type bound, out Type type){
        //Log($"instantiate {node.Name}");
        type = Types.Find(node.Name);
        if(bound == null && type == null) throw new InvOp(
            $"No matching type for <{node.Name}>"
        );
        // NOTE cannot instantiate bound if interface
        if(type == null) type = bound;
        if(type.IsArray){
            var etype = type.GetElementType();
            var count = node.ChildNodes.Count;
            return Array.CreateInstance(etype, count);
        }else{
            return Activator.CreateInstance(type);
        }
    }

    static string ReadTypeName(Elem elem)
    => elem.HasAttribute(TYPE) ? elem.GetAttribute(TYPE) : elem.Name;

    static string ReadFieldName(Elem elem)
    => elem.HasAttribute(TYPE) ? elem.Name : null;

}}
