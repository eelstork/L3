using System; using System.Collections;
using InvOp = System.InvalidOperationException;
using Doc = System.Xml.XmlDocument;
using Elem = System.Xml.XmlElement; using Node = System.Xml.XmlNode;
using static UnityEngine.Debug;

namespace Activ.XML{
public partial class XmlReader{

    const string TYPE = "t";
    bool ignoreErrors;

    public XmlReader(bool ignoreErrors){
        this.ignoreErrors = ignoreErrors;
    }

    public object Read(string arg, Type type=null){
        if(arg == null) return null;
        var doc = new Doc();
        doc.LoadXml(arg);
        return Read(doc.DocumentElement, null, type);
    }

    public object Read(string arg, object target){
        if(arg == null) return null;
        var doc = new Doc();
        doc.LoadXml(arg);
        return Read(doc.DocumentElement, target, target.GetType());
    }

    object Read(Node node, object target, Type type){
        string S = ReadTypeName(node as Elem);
        string T = type?.Name(out int gcount);
        //if(!MatchTypeNames(S, T)) throw new InvOp(
        //    $"Source type [{S}] (XML) does not match "
        //  + $"target type [{T}] (C#)"
        //);
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
        return ReadObject(node, target, type, ignoreErrors);
    }

    bool MatchTypeNames(string source, string target){
        if(source == null) return true;
        if(target == null) return true;
        target = target.Replace("[]", "-Array");
        return source == target;
    }

    // NOTE - if the argument object is not null, it is populated
    // from the given node; otherwse obj will be instantiated.
    object ReadObject(
        Node node, object obj, Type S, bool ignoreErrors
    ){
        Type T;
        if(obj != null){
            T = obj.GetType();
        }else{
            obj = Instantiate(node, S, out T);
        }
        int count = node.ChildNodes.Count;
        for(var i = 0; i < count; i++){
            var child = node.ChildNodes[i];
            var elem      = child as Elem;
            var fieldName = ReadFieldName(elem);
            if(string.IsNullOrEmpty(fieldName)){
                var value = Read(elem, null, null);
                AddChild(obj, value, i);
            }else{
                try{
                    var fieldType = T.FieldType(fieldName);
                    var value = Read(elem, null, fieldType);
                    var field = T.GetField(fieldName);
                    field.SetValue(obj, value);
                }catch(InvOp){
                    if(!ignoreErrors) throw;
                }
            }
        }
        return obj;
    }

    void AddChild(dynamic parent, dynamic value,  int index){
        if(parent.GetType().IsArray){
            parent[index] = value;
        }else{
            try{
                parent.Add (value);
            }catch(Exception){
                UnityEngine.Debug.LogError(
                    $"Cannot add {value} of type ({value.GetType()}) to {parent}"
                );
                throw;
            }
        }
    }

    object Instantiate(Node node, Type bound, out Type type){
        var elem = node as Elem;
        //Log($"instantiate {node.Name}");
        type = Types.Find(ReadTypeName(elem));
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

    string ReadTypeName(Elem elem)
    => elem.HasAttribute(TYPE) ? elem.GetAttribute(TYPE) : elem.Name;

    string ReadFieldName(Elem elem)
    => elem.HasAttribute(TYPE) ? elem.Name : null;

}}
