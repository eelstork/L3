using System.Collections.Generic;
using System.Linq;
using InvOp = System.InvalidOperationException;
using Activ.Util;

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

    override public string TFormat(bool ex){
        var pstr = ex ? " (...)"
                      : $" ({FormatParams(children)})";
        return (opt ? "opt " : null) + (function.None() ? "???" : function) + pstr
             + (once ? "once" : null);
    }

    public static string FormatParams(IEnumerable<Node> children){
        if(children == null) return null;
        var c = from x in children select x.TFormat(ex: false);
        return string.Join(", ", c);
    }

    override public void ReplaceChild(Node x, Node y){
        //var i = args.IndexOf(x);
        //args[i] = y;
        //y.SetParent(this);
        throw new InvOp();
    }

    override public Node[] children
    => args == null ? null
     : (from x in args select x as Node).ToArray();

     override public void DeleteChild(Node child)
     => args.Remove((Expression)child);

     override public void AddChild(Node child)
     => args.Add((Expression)child);

     override public string ToString() => TFormat(true);

}}
