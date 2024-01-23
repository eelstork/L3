using System; using System.Collections;
using Doc = System.Xml.XmlDocument;
using Elem = System.Xml.XmlElement;
using static UnityEngine.Debug;

namespace Activ.XML{
public static class XWriter{

    public static string Write(object obj){
        var doc = new Doc();
        var root = Write(obj, doc);
        doc.AppendChild(root);
        return doc.OuterXml;
    }

    static Elem Write(object obj, Doc doc){
        var type = obj.GetType();
        var elem = doc.CreateElement(GetTypeName(type));
        if(obj is string || type.IsPrimitive){
            elem.InnerText = obj.ToString();
        }else if(obj is IList) foreach(var x in (IEnumerable)obj){
            var child = Write(x, doc);
            elem.AppendChild(child);
        }else foreach(var field in type.GetFields()){
            if(field.IsStatic) continue;
            var value = field.GetValue(obj);
            var child = Write(value, doc);
            child.SetAttribute("field-name", field.Name);
            elem.AppendChild(child);
        }
        return elem;
    }

    // NOTE XML-unsupported chars in type names
    // - '[]' in array types
    // - backtick in generic types
    static string GetTypeName(Type type){
        if(type.IsArray){
            return type.Name.Replace("[]", "-Array");
        }else{
            var name = type.Name;
            var i = name.IndexOf("`");
            return i < 0 ? name : name.Substring(0, i);
        }
    }

}}
