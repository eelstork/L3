using System.Reflection;
using InvOp = System.InvalidOperationException;
using L3;

namespace R1{
public static class Call{

    public static object Invoke(
        L3.Call ca, Scope scope, Script cx
    ){
        cx.Log("call/" + ca);
        // 1. Find the wanted function,
        var node = scope?.Find(ca.name);
        MethodInfo cs = null;
        if(node == null){
            cs = ResolveCsFunc(ca.name, cx);
            if(cs == null){
                if(ca.opt){
                    return false;
                }else throw new InvOp(
                    $"No func or C# method matching {ca.name}"
                );
            }
        }
        // 2. Resolve args to sub-scope
        var sub = new Scope();
        foreach(var arg in ca.args){
            sub.Add(cx.Step(arg as Node) as Node);
        }
        // 3.2a If a C# binding, call the native function
        if(cs != null){
            return CSharp.Invoke(cs, sub, cx);
        }
        // 3.2b Otherwise Eval an L3 function in-scope.
        else{
            // Push the subscope
            cx.stack.Push( sub );
            var output = cx.Step(node);
            // Exit subscope and return the output
            cx.stack.Pop();
            return output;
        }
    }

    static MethodInfo ResolveCsFunc(string name, Script context)
    => context.GetType().GetMethod(name);

}}
