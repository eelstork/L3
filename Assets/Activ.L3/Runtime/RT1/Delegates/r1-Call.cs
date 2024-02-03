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
        // Load arguments into the subscope
        for(int i = 0; i < args.Length; i++){
            //ebug.Log($"Build arg {i} for {ca.function} with params [{func.parameters}]");
            var arg = new Arg(func.parameters[i].name, args[i]);
            sub.Add(arg);
        }
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

    static MethodInfo[] ResolveCsFunc(
        string name, object target, int count
    ) => target.GetType().GetMethods()
                        .Where(m => m.Name == name)
                        .Where(m => m.GetParameters().Length == count)
                        .ToArray();

}}
