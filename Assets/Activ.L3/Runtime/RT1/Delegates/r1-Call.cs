using System.Reflection;
using InvOp = System.InvalidOperationException;
using L3;
using System.Linq;
using UnityEngine;
using static UnityEngine.Debug;

namespace R1{
public static class Call{

    // NOTE - target may be null
    public static object Invoke(
        L3.Call ca, Context cx, object target
    ){
        // Resolve callable object
        var call = Res(ca, cx, target);
        // Resolve arguments
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
        // Invoke function
        return call switch{
            Function f
                => CallFunction(f, cx, target, args),
            MethodInfo[] m
                => CSharp.Invoke(m, args, target ?? cx.pose),
            _ => throw new InvOp($"Not callable: {call}")
        };
    }

    static object CallFunction(
        Function func, Context cx, object target, object[] args
    ){
        var sub = new Scope();
        cx.env.EnterCall(sub, target);
        //Debug.Log($"CALL simple function: [{node}]");
        var content = func.expression as Node;
        object output;
        if(content != null){
            output = cx.Step(content);
        }else{
            if(func.auto){
                return Solver.Find(func.type, args);
            }
            output = Token.@void;
        }
        // Exit subscope and return the output
        cx.env.ExitCall();
        return output;
    }

    static object Res(L3.Call ca, Context cx, object target){
        var name = ca.function;
        var func = cx.FindFunction(name);
        if(func != null) return func;
        var csfunc = ResolveCsFunc(
            name, target ?? cx.pose, ca.args.Count
        );
        if(csfunc != null) return csfunc;
        if(ca.opt){
            return false;
        }else throw new InvOp(
            $"No func or C# method matching {name}"
        );
    }

    // NOTE - target may be null
    /*
    public static object Invoke(
        L3.Call ca, Context cx, object target
    ){
        cx.Log("call/" + ca);
        var name = ca.function;
        // Find the wanted function,
        var node = cx.FindFunction(name);
        MethodInfo[] cs = null;
        if(node == null){
            cs = ResolveCsFunc(name, target ?? cx.pose, ca.args.Count);
            if(cs == null){
                if(ca.opt){
                    return false;
                }else throw new InvOp(
                    $"No func or C# method matching {name}"
                );
            }
        }
        // Resolve args to sub-scope
        var args = new object[ca.args.Count];
        for(var i = 0; i < args.Length; i++){
            args[i] = cx.Step(ca.args[i] as Node);
        }
        if(cs != null && cs.Length > 0){  // (C#) native call
            return CSharp.Invoke(cs, args, target ?? cx.pose);
        }else{                            // L3 call
            if(node == null) throw new InvOp(
                $"Function not found {ca.function}"
            );
            // TODO - need another method to push arguments
            // Push the subscope
            var func = node as Function;
            var sub = new Scope();
            for(int i = 0; i < args.Length; i++){
                //ebug.Log($"Build arg {i} for {ca.function} with params [{func.parameters}]");
                var arg = new Arg(func.parameters[i].name, args[i]);
                sub.Add(arg);
            }
            cx.env.EnterCall(sub, target);
            //Debug.Log($"CALL simple function: [{node}]");
            var co = func.expression as Node;
            object output;
            if(co != null){
                output = cx.Step(co);
            }else{
                output = Token.@void;
            }
            // Exit subscope and return the output
            cx.env.ExitCall();
            return output;
        }
    }*/

    static MethodInfo[] ResolveCsFunc(
        string name, object target, int count
    ) => target.GetType().GetMethods()
                        .Where(m => m.Name == name)
                        .Where(m => m.GetParameters().Length == count)
                        .ToArray();

}}
