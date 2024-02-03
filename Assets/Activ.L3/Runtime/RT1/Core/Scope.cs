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

}}
