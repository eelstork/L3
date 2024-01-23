using UnityEngine;
using System.Collections.Generic;
using InvOp = System.InvalidOperationException;
using System.Reflection;

namespace L3{
[ExecuteInEditMode]
public class Script : MonoBehaviour{

    public L3.Unit main;
    public Stack<Scope> stack = new ();
    public bool onDemand = true;
    public bool runOnce;
    public Record record = new ();

    public Scope scope => stack.Count == 0 ? null : stack.Peek();

    public void Update(){
        if(onDemand){
            if(runOnce){ runOnce = false; Step(main.value); }
        }else{
            Step(main.value);
        }
    }

    object Step(Node exp){
        record.Enter(exp);
        var x = exp switch{
            Composite co => Step(co),
            Call ca      => Invoke(ca),
            LString str  => str,
            Number num   => num,
            _            => throw new InvOp($"Unknown {exp}"),
        };
        record.Exit(exp, value: x);

        return x;
    }

    // ---------------------------------------

    object Step(Function func){
        Log("fu/" + func);
        return Step(func.expression as Node);
    }

    // TODO BT style composites not implemented
    object Step(Composite co){
        Log("co/" + co);
        foreach(var k in co.nodes){
            Step(k as Node);
        }
        return null;
    }

    object Invoke(Call ca){
        Log("call/" + ca);
        // 1. Find the wanted function,
        var node = scope?.Find(ca.name);
        MethodInfo cs = null;
        if(node == null){
            cs = ResolveCsFunc(ca.name);
            if(cs == null){
                Debug.Log($"No func or C# method matching {ca.name}");
                return null;
            }
        }
        // 2. Resolve args to sub-scope
        var sub = new Scope();
        foreach(var arg in ca.args){
            sub.Add(Step(arg as Node) as Node);
        }
        // 3.2 If a native binding, call the native function
        if(cs != null){
            return CSharp.Invoke(cs, sub, this);
        }else{
            // 3.1 Eval the function in-scope.
            stack.Push( sub );
            var output = Step(node);
            // 4. Exit sub-scope and return the output
            stack.Pop();
            return output;
        }
    }

    MethodInfo ResolveCsFunc(string name){
        var type = GetType();
        var method = type.GetMethod(name);
        //if(method != null){
        //    Log($"Did resolve as {method}");
        //}
        return method;
    }

    // ---------------------------------------

    public void Log(object arg)
    => UnityEngine.Debug.Log(arg);

}}
