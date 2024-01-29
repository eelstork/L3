using System.Collections.Generic;
using System.Linq;

namespace L3{
public partial class Unit : AbstractBranch<Node>{

    public string @namespace = "";
    public List<string> _using;

    override public string TFormat()
    => $"namespace: {@namespace}";

     [EditorAction]
     public void AddUsing(){
         if(_using == null) _using = new ();
         _using.Add("USING...");
     }

     [EditorAction]
     public void AddField()
     => AddChild(new Field());

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
