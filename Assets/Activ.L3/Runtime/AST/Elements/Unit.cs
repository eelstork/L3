using System.Collections.Generic; using System.Linq; using Activ.Util;
using static UnityEngine.Debug;

namespace L3{
public partial class Unit : AbstractBranch<Node>{

    public string ns = "";
    public string deps = "";
    public string @as = "";
    public bool once;
    public bool isTest;
    public string expectedError = "";

    override public string TFormat(bool ex){
        string str = null;
        if(once) str += "once";
        if(!ns.None()) str += $"namespace {ns}:";
        if(!@as.None()) str += $", as {@as}:";
        if(str == null) str = "unit:";
        if(str.StartsWith(", ")) str = str.Substring(2);
        return str;
    }

     [eda] public void AddUsing(){
         if(deps == null) deps = "";
     }

     [eda] public void AddCall() => AddChild(new Call());
     [eda] public void AddComposite() => AddChild(new Composite());
     [eda] public void AddClass() => AddChild(new Class());
     [eda] public void AddField() => AddChild(new Field());
     [eda] public void AddFunction() => AddChild(new Function());
     [eda] public void AddNew() => AddChild(new New());
     [eda] public void AddStatement(){
         Log("Add statement");
     }

     override public string ToString() => $"Unit (ns:{ns})";

}}
