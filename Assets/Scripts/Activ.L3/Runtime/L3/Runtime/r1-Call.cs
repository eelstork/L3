using System.Reflection;
using InvOp = System.InvalidOperationException;
using L3;
using System.Linq;
using UnityEngine;

namespace R1{
public static class Call{

    // NOTE - target may be null
    public static object Invoke(
        L3.Call ca, Scope scope, Context cx, object target
    ){
        cx.Log("call/" + ca);
        var name = ca.function;
        // Find the wanted function,
        var node = scope?.Find(name);
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
        var sub = new Scope();
        foreach(var arg in ca.args){
            sub.Add(cx.Step(arg as Node) as Node);
        }
        if(cs != null && cs.Length > 0){  // (C#) native call
            return CSharp.Invoke(cs, sub, target ?? cx);
        }else{                            // L3 call
            // Push the subscope
            cx.stack.Push( sub );
            Debug.Log($"CALL simple function: [{node}]");
            var output = cx.Step(node);
            // Exit subscope and return the output
            cx.stack.Pop();
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
