using System.Collections.Generic;
using System.Linq;

namespace L3{
public partial class New : Branch, Expression{

    public string type = "";
    public bool opt = false;

    [Hierarchy]
    public List<Expression> args = new ();

    public int argcount => args?.Count ?? 0;

    [eda] public void AddString() => args.Add(new LString());

    [eda] public void AddNumber() => args.Add(new Number());

    [eda] public void AddVar() => args.Add(new Var());

    override public string TFormat(bool ex){
        var pstr = ex ? " (...)"
                      : $" ({Call.FormatParams(children)})";
        return (opt ? "opt " : null) + "new "+ type + pstr;
    }

    override public Node[] children
    => args == null ? null
     : (from x in args select x as Node).ToArray();

     override public void DeleteChild(Node child)
     => args.Remove((Expression)child);

     override public void AddChild(Node child)
     => args.Add((Expression)child);

     override public void ReplaceChild(Node x, Node y){
         var i = args.IndexOf(x as Expression);
         args[i] = y as Expression;
         y.SetParent(this);
     }

}}
