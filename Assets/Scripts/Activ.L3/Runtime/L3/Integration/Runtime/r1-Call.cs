using System.Reflection;
using InvOp = System.InvalidOperationException;
using L3;
using System.Linq;

namespace R1{
public static class Call{

    // NOTE - target may be null
    public static object Invoke(
        L3.Call ca, Scope scope, Context cx, object target
    ){
        cx.Log("call/" + ca);
        var name = ca.function;
        // 1. Find the wanted function,
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
        // 2. Resolve args to sub-scope
        var sub = new Scope();
        foreach(var arg in ca.args){
            sub.Add(cx.Step(arg as Node) as Node);
        }
        // 3.2a If a C# binding, call the native function
        if(cs != null && cs.Length > 0){
            return CSharp.Invoke(cs, sub, target ?? cx);
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

    static MethodInfo[] ResolveCsFunc(
        string name, object context, int count
    ) => context.GetType().GetMethods()
                        .Where(m => m.Name == name)
                        .Where(m => m.GetParameters().Length == count)
                        .ToArray();

}}
