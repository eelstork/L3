using static UnityEngine.Debug;

namespace L3{
public partial class Var : Node, Expression{

    public string value = "";
    public bool opt = false;

    override public string TFormat(bool ex) => value;

    override public string ToString()
    => $"L3.Var{{{value}, opt: {opt}}}";

    [EditorAction]
    public void Call(){
        Log("Should make into a call");
        if(parent == null) LogWarning("NO PARENT");
        // When we make a var into a call:
        // access
        //   var
        //   call
        var branch = new Composite();
        branch.type = Composite.Type.access;
        branch.AddChild(this);
        var call = new Call();
        branch.AddChild(call);
        parent.ReplaceChild(this, branch);
        //if(nodes == null) nodes = new ();
        //nodes.Add(new Call());
    }

}}
