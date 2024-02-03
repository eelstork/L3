using System.Collections.Generic;
using System.Linq;

namespace L3{
public partial class New : Branch, Expression{

    public string type = "";
    public bool opt = false;

    [Hierarchy]
    public List<Expression> args = new ();

    [EditorAction]
    public void AddString() => args.Add(new LString());

    [EditorAction]
    public void AddNumber() => args.Add(new Number());

    [EditorAction]
    public void AddVar() => args.Add(new Var());

    override public string TFormat()
    => (opt ? "opt " : null) + "new " +  type + "(...)";

    override public Node[] children
    => args == null ? null
     : (from x in args select x as Node).ToArray();

     override public void DeleteChild(Node child)
     => args.Remove((Expression)child);

     override public void AddChild(Node child)
     => args.Add((Expression)child);

}}
