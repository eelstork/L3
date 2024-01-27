using UnityEngine;
using System.Collections.Generic;
using InvOp = System.InvalidOperationException;

namespace L3{
[ExecuteInEditMode]
public class L3Component : MonoBehaviour, Context{

    public L3.L3Script main;
    public Stack<Scope> stack = new ();
    public bool onDemand = true, runOnce;
    public Record record = new ();

    public Scope scope => stack.Count == 0 ? null : stack.Peek();

    public object Step(Node exp){
        record.Enter(exp);
        var x = exp switch{
            Composite co => R1.Composite.Step(co, this),
            Call      ca => R1.Call.Invoke(ca, scope, this),
            Function  fu => R1.Func.Step(fu, this),
            Literal   li => li,
            Var       va => R1.Var.Resolve(va, this),
            _            => throw new InvOp($"Unknown {exp}"),
        };
        record.Exit(exp, value: x);
        return x;
    }

    void Update(){
        if(onDemand){
            if(runOnce){ runOnce = false; Step(main.value); }
        }else{
            Step(main.value);
        }
    }

    // ----

    Stack<Scope> Context.stack => stack;

    // TEMP FOR TESTING ONLY ----------------------------------------

    public Transform target;

    public void MoveTo(Transform target){
        //Log($"Move to [{target}]");
    }

    //public void Log(object arg)
    //=> UnityEngine.Debug.Log(arg);

}}
