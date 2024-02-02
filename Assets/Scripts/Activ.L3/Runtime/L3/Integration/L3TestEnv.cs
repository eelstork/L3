using UnityEngine;
using System.Collections.Generic;
using InvOp = System.InvalidOperationException;
using R1;

namespace L3{
public class L3TestEnv : Context{

    public L3Script main;
    public Env env;
    public Record record = new ();
    public object pose = new Logger();

    Env Context.env => env;
    object Context.pose{ get => pose; set => pose = value; }

    public L3TestEnv(L3Script main) => this.main = main;

    public void Exec(){
        try{
            Exec(main.value);
        }catch(System.Exception){ env.Dump(); throw; }
    }

    public Node FindFunction(string name)
    => env.FindFunction(name) ?? FindFuncHere(name);

    public Node FindFuncHere(string name){
        var unit = main.value;
        foreach(var x in unit.children){
            var f = x as Function;
            if(f==null) continue;
            if(f.name == name) return f;
        }
        return null;
    }

    public object Step(Node exp, HashSet<Node> deps){
        var x = exp switch{
            Unit      un => R1.Unit.Step(un, this, deps),
            _            => Step(exp)
        };
        return x;
    }

    public object Step(Node exp){
        record.Enter(exp);
        try{
            var x = exp switch{
                Composite co => R1.Composite.Step(co, this),
                Call      ca => R1.Call.Invoke(ca, this, null),
                New       nw => R1.New.Invoke(nw, this),
                Unit      un => R1.Unit.Step(un, this, new ()),
                Literal   li => li.value,
                L3.Var    va => R1.Var.Resolve(va, this),
                // TODO if the below is Ref, this cannot be Step
                Field     fi => R1.Field.Step(fi, this),
                Dec       dc => R1.Dec.Step(dc, this),
                _            => throw new InvOp($"Unknown construct [{exp}]"),
            };
            record.Exit(exp, value: x);
            return x;
        }catch(System.Exception ex){
            record.ExitWithError(exp, ex);
            throw;
        }
    }

    public object Ref(Node exp){
        record.Enter(exp);
        var x = exp switch{
            Composite co => R1.Composite.Ref(co, this),
            L3.Var    va => R1.Var.Refer(va, this),
            Field     fi => R1.Field.Step(fi, this),
            _            => throw new InvOp($"Cannot ref [{exp}]"),
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

    void Exec(Unit unit){
        Activ.Util.Types.SetCustomTypes(TypeMap.types);
        record.frame = null;
        if(env == null) env = new ();
        env.Enter();
        Step(unit);
        env.Exit();
    }

    class Logger{
        public void Log(object arg) => UnityEngine.Debug.Log(arg);
    }

}}
