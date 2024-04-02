using L3; using Co = L3.Composite;
using static L3.Composite.Type;
using static L3.Token;
using static L3.Statuses;
using Activ.Util.BT;
using Activ.Util;

namespace R1{
public static partial class Composite{

    public static object OrderedSeq(Co co, Context cx){
        // TODO - no real justification for this even after noop
        if(co.nodes.Empty()) return status.done;
        //cx.Log($"seq/{co} in context {cx}");
        var index = cx.GetIndex(co);
        var node = co.nodes[index];
        var val = cx.Step(node as Node);
        var count = co.nodes.Count;
        if(IsDone(val)){
            cx.SetIndex(co, (index + 1) % co.nodes.Count );
            if(index < count - 1) return cont;
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
        var count = co.nodes.Count;
        if(IsFailing(val)){
            cx.SetIndex(co, (index + 1) % co.nodes.Count );
            if(index < count - 1) return cont;
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
        var count = co.nodes.Count;
        if(IsFailing(val) || IsDone(val)){
            cx.SetIndex(co, (index + 1) % co.nodes.Count );
            if(index < count - 1) return cont;
        }
        return val;
    }

}}
