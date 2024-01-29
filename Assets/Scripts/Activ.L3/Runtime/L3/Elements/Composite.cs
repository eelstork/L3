using System.Collections.Generic;
using System.Linq;

namespace L3{
public partial class Composite : Branch, Expression{

    public enum Type{block, sel, seq, act, assign, access};

    public Type type;
    public string name;

    [Hierarchy]
    public List<Expression> nodes;

    public void Delete(){}

    [EditorAction]
    public void AddCall(){
        if(nodes == null) nodes = new ();
        nodes.Add(new Call());
    }

    [EditorAction]
    public void AddComposite(){
        if(nodes == null) nodes = new ();
        nodes.Add(new Composite());
    }

    [EditorAction]
    public void AddVar(){
        if(nodes == null) nodes = new ();
        nodes.Add(new Var());
    }

    [EditorAction]
    public void AddNum(){
        if(nodes == null) nodes = new ();
        nodes.Add(new Number());
    }

    [EditorAction]
    public void AddField(){
        if(nodes == null) nodes = new ();
        nodes.Add(new Field());
    }

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
         Type.assign => "= ",
         Type.access => ". ",
         _ => null
     };

}}
