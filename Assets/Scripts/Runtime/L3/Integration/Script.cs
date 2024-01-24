using UnityEngine;
using System.Collections.Generic;
using InvOp = System.InvalidOperationException;

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

    public object Step(Node exp){
        record.Enter(exp);
        var x = exp switch{
            Composite co => R1.Composite.Step(co, this),
            Call ca      => R1.Call.Invoke(ca, scope, this),
            Function fu  => R1.Func.Step(fu, this),
            Literal lit  => lit,
            _            => throw new InvOp($"Unknown {exp}"),
        };
        record.Exit(exp, value: x);
        return x;
    }

    // ---------------------------------------

    public void Log(object arg)
    => UnityEngine.Debug.Log(arg);

}}
