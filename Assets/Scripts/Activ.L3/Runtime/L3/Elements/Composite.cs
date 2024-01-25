using System.Collections.Generic;
using System.Linq;

namespace L3{
public partial class Composite : Branch, Expression{

    public enum Type{block, sel, seq, act};

    public Type type;

    [Hierarchy]
    public List<Expression> nodes;

    [EditorAction]
    public void AddCall(){
        if(nodes == null) nodes = new ();
        nodes.Add(new Call());
    }

    public void Delete(){}

    override public string TFormat()
    => type + ": " + name;

    override public void AddChild(Node child){
        if(nodes == null) nodes = new ();
        nodes.Add((Expression)child);
    }

    override public void DeleteChild(Node child){
        if(nodes == null) return;
        nodes.Remove((Expression)child);
    }

    override public Node[] children
    => nodes == null ? null
     : (from x in nodes select x as Node).ToArray();

     override public string childPrefix => type switch{
         Type.sel => "|| ",
         Type.seq => "&& ",
         Type.act => "%% ",
         _ => null
     };

}}
