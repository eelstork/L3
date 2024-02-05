using L3; using Co = L3.Composite;
using static L3.Composite.Type;
using static L3.Token;
using static L3.Statuses;

namespace R1{
public static partial class Composite{

    public static object OrderedSeq(Co co, Context cx){
        //cx.Log($"seq/{co} in context {cx}");
        var index = cx.GetIndex(co);
        var node = co.nodes[index];
        var val = cx.Step(node as Node);
        if(IsDone(val)){
            cx.SetIndex(co, (index + 1) % co.nodes.Count );
        }else if(IsFailing(val)){
            cx.SetIndex(co, 0);
        }
        return val;
    }

    public static object OrderedSel(Co co, Context cx){
        //cx.Log($"seq/{co} in context {cx}");
        var index = cx.GetIndex(co);
        var node = co.nodes[index];
        var val = cx.Step(node as Node);
        if(IsFailing(val)){
            cx.SetIndex(co, (index + 1) % co.nodes.Count );
        }else if(IsDone(val)){
            cx.SetIndex(co, 0);
        }
        return val;
    }

    public static object OrderedAct(Co co, Context cx){
        //cx.Log($"seq/{co} in context {cx}");
        var index = cx.GetIndex(co);
        var node = co.nodes[index];
        var val = cx.Step(node as Node);
        if(IsFailing(val) || IsDone(val)){
            cx.SetIndex(co, (index + 1) % co.nodes.Count );
        }
        return val;
    }

}}
