using System; using System.Collections;
using System.Xml;
using System.Collections.Generic;
using System.Reflection;
using Field = System.Reflection.FieldInfo;

namespace L3{
public class XMLWriter{

    public string ToXML(Node arg){
        var doc = new XmlDocument();
        Write(arg, doc);
        return doc.OuterXml;
    }

    void Write(object arg, XmlDocument doc, XmlElement parent=null){
        var type = arg.GetType();
        var e = doc.CreateElement(type.Name);
        if(parent == null) doc.AppendChild(e);
        else parent.AppendChild(e);
        foreach(var field in type.GetFields()){
            if(IsHidden(field)) continue;
            var name = field.Name;
            var value = field.GetValue(arg);
            if(value == null) continue;
            switch(value){
                case IList list:
                    WriteList(name, list, doc, e); break;
                default:
                    e.SetAttribute(name, value.ToString()); break;
            }
        }
        var branch = arg as Branch;
        if(branch == null || branch.children==null) return;
        foreach(var child in branch.children){
            Write(child, doc, parent: e);
        }
    }

    void WriteList(
        string name, IList list, XmlDocument doc, XmlElement parent
    ){
        var e = doc.CreateElement(name);
        parent.AppendChild(e);
        foreach(var child in list){
            Write(child, doc, parent: e);
        }
    }

    bool IsHidden(Field arg)
    => Attribute.IsDefined(arg, typeof(HierarchyAttribute));

}}
