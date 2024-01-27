using System.Collections.Generic;
using System.Linq;

namespace L3{
public partial class Unit : Branch{

    public List<string> @using;

    [Hierarchy]
    public List<Expression> nodes;

    override public string TFormat() => "Unit";

    override public Node[] children
    => nodes == null ? null
     : (from x in nodes select x as Node).ToArray();

     override public void DeleteChild(Node child){
         if(nodes == null) return;
         nodes.Remove((Expression)child);
     }

     override public void AddChild(Node child){
         if(nodes == null) nodes = new ();
         nodes.Add((Expression)child);
     }

     [EditorAction]
     public void AddUsing(){
         if(@using == null) @using = new ();
         @using.Add("USING...");
     }

     [EditorAction]
     public void AddClass()
     => AddChild(new Class());

     [EditorAction]
     public void AddFunction()
     => AddChild(new Function());

     [EditorAction]
     public void AddComposite()
     => AddChild(new Composite());

}}
