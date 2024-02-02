using System.Collections.Generic;
using System.Linq;
using L3;

namespace R1{
public class Scope{

    List<Node> nodes = new ();

    public List<Node> _nodes => nodes;

    public void Add(Node arg){
        nodes.Add(arg);
    }

    public Node Find(string name){
        foreach(var x in nodes){
            var named = x as Named;
            if(named == null) continue;
            if(named.name == name) return x;
        }
        return null;
    }

    public object[] Unwrap()
    => (from node in nodes select Unwrap(node)).ToArray();

    // TODO remove if possible
    public object Unwrap(Node arg){
        switch(arg){
            case Number   n: return n.value;
            case LString  s: return s.value;
            case FieldRef o: return o.value;
            case Variable x: return x.value;
            default: return arg;
        }
    }

}}
