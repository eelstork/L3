using System.Reflection;
using InvOp = System.InvalidOperationException;
using L3;
using System.Linq;
using UnityEngine;

namespace R1{
public static class Call{

    // NOTE - target may be null
    public static object Invoke(
        L3.Call ca, Context cx, object target
    ){
        cx.Log("call/" + ca);
        var name = ca.function;
        // Find the wanted function,
        var node = cx.FindFunction(name);
        MethodInfo[] cs = null;
        if(node == null){
            cs = ResolveCsFunc(name, target ?? cx, ca.args.Count);
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
        //}
        //foreach(var arg in ca.args){
        //    sub.Add(cx.Step(arg as Node) as Node);
        //}
        if(cs != null && cs.Length > 0){  // (C#) native call
            return CSharp.Invoke(cs, args, target ?? cx);
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
    }

    static MethodInfo[] ResolveCsFunc(
        string name, object context, int count
    ) => context.GetType().GetMethods()
                        .Where(m => m.Name == name)
                        .Where(m => m.GetParameters().Length == count)
                        .ToArray();

}}
