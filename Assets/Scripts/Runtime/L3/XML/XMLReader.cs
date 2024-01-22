using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace L3{
public class XMLReader{

    public Node FromXML(string arg){
        if(arg == null) return null;
        var doc = new XmlDocument();
        doc.LoadXml(arg);
        foreach(XmlNode node in doc.ChildNodes){
            return ToNode(node) as Node;
        }
        return null;
    }

    object ToNode(XmlNode arg){
        object node = Instantiate(arg);
        foreach(XmlAttribute attr in arg.Attributes){
            var name = attr.Name;
            var value = attr.Value;
            var type = node.GetType();
            var field = type.GetField(name);
            if(field != null){
                switch(field.FieldType.Name){
                    case "Boolean":
                        field.SetValue(node, bool.Parse(value));
                        break;
                    case "String":
                        field.SetValue(node, value);
                        break;
                    case "Single":
                        field.SetValue(node, float.Parse(value));
                        break;
                    default:
                        Debug.LogError(
                            $"Unrecognized field type {field.FieldType.Name}"
                        );
                        break;
                }
            }else{
                Debug.LogWarning($"No such field: {name} in {node}");
            }
        }
        foreach(XmlNode child in arg.ChildNodes){
            var childNode = ToNode(child);
            if(childNode != null){
                switch(node){
                    case Branch branch:
                        var c1 = childNode as Node;
                        var c2 = childNode as List<Parameter>;
                        if(c1 != null){
                            branch.AddChild(c1);
                        }else if(c2 != null){
                            var func = node as Function;
                            func.parameters = c2;
                        }else{
                            Debug.LogWarning($"{childNode} is not an L3 Node in {node}");
                        }
                        break;
                    case List<Parameter> parameters:
                        parameters.Add((Parameter)childNode);
                        break;
                    case null:
                        Debug.LogWarning($"Invalid parent [null] for child [{childNode}]");
                        break;
                    default:
                        Debug.LogWarning($"Invalid parent [{node}] of type {node.GetType().Name} for child [{childNode}]");
                        break;
                }
            }
        }
        return node;
    }

    object Instantiate(XmlNode arg){
        switch(arg.Name){
            //case "args": return new List<Expression>();
            case "Call": return new Call();
            case "Composite": return new Composite();
            case "Function": return new Function();
            case "Number": return new Number();
            case "Parameter": return new Parameter();
            case "parameters": return new List<Parameter>();
            case "String": return new String();
            case "Var": return new Var();
            default:
                Debug.LogWarning($"Cannot instantiate {arg.Name}");
                return null;
        }
    }

}}
