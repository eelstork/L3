using System.Collections.Generic;
using System.Linq;

namespace L3{
public class Scope{

    List<Node> nodes = new ();

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

    public object Unwrap(Node arg){
        switch(arg){
            case Number    n: return n.value;
            case LString   s: return s.value;
            case L3.Object o: return o.value;
            default: return arg;
        }
    }

}}
