using System.Collections.Generic; using System.Reflection;
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
        var n = ca.args.Count; var args = new object[n];
        for(var i = 0; i < n; i++){
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

    public static object[] BuildArgs(List<Expression> @in, Context cx){
        var n = @in.Count; var args = new object[n];
        for(var i = 0; i < n; i++){
            //Debug.Log($"Build arg, where {@in[i]} of type {@in[i].GetType()}");
            args[i] = cx.Step(@in[i] as Node);
        } return args;
    }

}}
