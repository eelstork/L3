using System.Collections.Generic;
using System.Linq;

namespace L3{
public partial class Call : Branch, Expression{

    public string function = "";
    public bool once;
    public bool opt = false;

    [Hierarchy]
    public List<Expression> args = new ();

    [EditorAction]
    public void AddString() => args.Add(new LString());

    [EditorAction]
    public void AddNumber() => args.Add(new Number());

    [EditorAction]
    public void AddComposite() => args.Add(new Composite());

    [EditorAction]
    public void AddVar() => args.Add(new Var());

    [EditorAction]
    public void AddCall() => args.Add(new Call());

    override public string TFormat()
    => (opt ? "opt " : null) + function + "(...) "
    + (once ? "once" : null);

    override public Node[] children
    => args == null ? null
     : (from x in args select x as Node).ToArray();

     override public void DeleteChild(Node child)
     => args.Remove((Expression)child);

     override public void AddChild(Node child)
     => args.Add((Expression)child);

     override public string ToString() => TFormat();

}}
