using System.Reflection;
using InvOp = System.InvalidOperationException;
using L3;
using System.Linq;
using UnityEngine;
using static UnityEngine.Debug;

namespace R1{
public static class Call{

    public static object Invoke(
        L3.Call ca, Context cx, object target
    ){
        // Evaluate arguments
        var args = new object[ca.args.Count];
        for(var i = 0; i < args.Length; i++){
            args[i] = cx.Step(ca.args[i] as Node);
        }
        if(ca.once){
            // NOTE - this should return 'done'... probably.
            //Log($"Should check {cx.history} for 'once'");
            if(cx.history.DidCall(ca.function, args))
                return null;
        }
        return cx.CallFunction(target, ca.function, args);
    }

}}
