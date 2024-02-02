using System.Collections.Generic;
using System.Linq;

namespace L3{
public partial class Unit : AbstractBranch<Node>{

    public string ns = "";
    public string deps = "";
    public string @as = "";
    public bool isTest;
    public string expectedError = "";

    override public string TFormat(){
        string str = null;
        if(!ns.None()) str += $"namespace {ns}:";
        if(!@as.None()) str += $", as {@as}:";
        if(str == null) str = "unit:";
        if(str.StartsWith(", ")) str = str.Substring(2);
        return str;
    }

     [EditorAction]
     public void AddUsing(){
         if(deps == null) deps = "";
     }

     [EditorAction]
     public void AddCall()
     => AddChild(new Call());

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
     public void AddNew()
     => AddChild(new New());

     [EditorAction]
     public void AddComposite()
     => AddChild(new Composite());

     override public string ToString(){
         return $"Unit (ns:{ns})";
     }

}}
