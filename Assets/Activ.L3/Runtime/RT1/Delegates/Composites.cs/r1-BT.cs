using System.Reflection;
using L3; using Co = L3.Composite;
using static L3.Composite.Type;
using static L3.Token;
using InvOp = System.InvalidOperationException;
using UnityEngine;
using static L3.Statuses;

namespace R1{
public static partial class Composite{

    public static object Sel(Co co, Context cx){
        if(co.ordered) return OrderedSel(co, cx);
        object val = null;
        foreach(var k in co.nodes){
            val = cx.Step(k as Node);
            if(IsDone(val) || IsCont(val)){
                Debug.Log($"Exit selector {co.name} cause {val}");
                return val;
            }
        }
        return val;
    }

    public static object Seq(Co co, Context cx){
        if(co.ordered) return OrderedSeq(co, cx);
        object val = null;
        foreach(var k in co.nodes){
            val = cx.Step(k as Node);
            if(IsFailing(val) || IsCont(val)){
                return val;
            }
        }
        return val;
    }

    public static object Act(Co co, Context cx){
        //cx.Log("act/" + co);
        object val = null;
        foreach(var k in co.nodes){
            val = cx.Step(k as Node);
            if(IsCont(val)) return val;
        }
        return val;
    }

}}
