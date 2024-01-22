using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public partial class Call : Branch, Expression{

    [Hierarchy]
    public List<Expression> args = new ();

    [EditorAction]
    public void AddString() => args.Add(new String());

    [EditorAction]
    public void AddNumber() => args.Add(new Number());

    [EditorAction]
    public void AddVar() => args.Add(new Var());

    override public Node[] children
    =>  args == null ? null
     : (from x in args select x as Node).ToArray();

     override public void AddChild(Node child)
     => args.Add((Expression)child);

}
