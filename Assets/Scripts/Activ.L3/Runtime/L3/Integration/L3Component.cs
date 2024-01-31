using UnityEngine;
using System.Collections.Generic;
using InvOp = System.InvalidOperationException;

namespace L3{
[ExecuteInEditMode]
public class L3Component : MonoBehaviour, Context{

    public L3.L3Script main;
    //public Stack<Scope> scope = new ();
    //public Stack<Scope> stack = new ();
    public Env env;
    public bool onDemand = true, runOnce;
    public Record record = new ();

    //public Scope scope => stack.Count == 0 ? null : stack.Peek();

    Env Context.env => env;

    public object Step(Node exp, HashSet<Node> deps){
        var x = exp switch{
            Unit      un => R1.Unit.Step(un, this, deps),
            _            => Step(exp)
        };
        return x;
    }

    public object Step(Node exp){
        record.Enter(exp);
        var x = exp switch{
            Composite co => R1.Composite.Step(co, this),
            Call      ca => R1.Call.Invoke(ca, this, null),
            New       nw => R1.New.Invoke(nw, this, null),
            Unit      un => R1.Unit.Step(un, this, new HashSet<Node>()),
            Literal   li => li,
            Var       va => R1.Var.Resolve(va, this),
            Field     fi => R1.Field.Step(fi, this),
            Dec       dc => R1.Dec.Step(dc, this),
            _            => throw new InvOp($"Unknown construct [{exp}]"),
        };
        record.Exit(exp, value: x);
        return x;
    }

    public object Instantiate(Class clss){
        record.Enter(clss);
        Debug.Log($"INSTANTIATE {clss}");
        object x = new R1.Obj(clss);
        record.Exit(clss, value: x);
        return x;
    }

    void Update(){
        if(onDemand){
            if(runOnce){ runOnce = false; Exec(main.value); }
        }else{
            Exec(main.value);
        }
    }

    void Exec(Unit unit){
        record.frame = null;
        if(env == null) env = new ();
        env.Enter();
        Step(unit);
        env.Exit();
    }

    // ----

    //Stack<Scope> Context.stack => stack;

    // TEMP FOR TESTING ONLY ----------------------------------------

    public int index = 2;
    public Range range = new ();

    public Transform target;

    public void MoveTo(Transform target){
        //Log($"Move to [{target}]");
    }

    [System.Serializable]
    public class Range{
        public int min = -5, max = 5;
        public void Reset(){
            min = -1; max = 1;
        }
        public void Reset(float w){
            min = max = (int) w;
        }
    }

    public void Log(object arg)
    => UnityEngine.Debug.Log(arg);

}}
