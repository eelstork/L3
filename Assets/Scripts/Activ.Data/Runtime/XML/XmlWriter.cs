using UnityEngine;
using System; using System.Collections;
using Doc = System.Xml.XmlDocument;
using Elem = System.Xml.XmlElement;
using static UnityEngine.Debug;

namespace Activ.XML{
public static class XmlWriter{

    public static string Write(object obj){
        var doc = new Doc();
        //Debug.Log($"Write [{obj}]") ;
        var root = Write(
            obj, GetTypeName(obj.GetType()), doc, skipTypeAttr:true
        );
        doc.AppendChild(root);
        return doc.OuterXml;
    }

    static Elem Write(
        object obj, string name, Doc doc, bool skipTypeAttr=false
    ){
        var type = obj.GetType();
        var elem = doc.CreateElement(name);
        if(!skipTypeAttr) elem.SetAttribute("t", GetTypeName(type));
        if(obj.GetType().IsEnum){
            elem.InnerText = obj.ToString();
        }else
        if(obj is string || type.IsPrimitive){
            elem.InnerText = obj.ToString();
        }else if(obj is IList) foreach(var x in (IEnumerable)obj){
            var child = Write(
                x, GetTypeName(x.GetType()),
                doc, skipTypeAttr: true);
            elem.AppendChild(child);
        }else foreach(var field in type.GetFields()){
            if(field.IsStatic) continue;
            // NOTE needed since made this private
            //if(field.Name == "xml") continue;
            var value = field.GetValue(obj);
            if(value == null) continue;
            var child = Write(value, field.Name, doc);
            //child.SetAttribute("field-name", field.Name);
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
