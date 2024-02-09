using System.Collections.Generic; using System.Linq;
using L3; using InvOp = System.InvalidOperationException;

namespace R1{
public class Scope{

    List<Node> nodes = new ();

    public List<Node> _nodes => nodes;

    public void Add(Node arg){
        if(!(arg is Named)) throw new InvOp($"Anonymous arg {arg}");
        nodes.Add(arg);
    }

    public ValueHolder FindValueHolder(string @var){
        foreach(var x in nodes){
            var named = x as Named;
            if(x is ValueHolder && named.name == @var)
            { return x as ValueHolder; }
        } return null;
    }

    // NOTE this method will not return null since only
    // named nodes are returned.
    // TODO if possible directly return the associate value...
    // well, for referencing purposes we may need the node.
    public Node Find(string name){
        foreach(var x in nodes){
            var named = x as Named;
            if(named.name == name){
                return x;
            }
        }
        return null;
    }

}}
