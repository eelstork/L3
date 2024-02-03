using UnityEngine;
using System.Collections.Generic;
using InvOp = System.InvalidOperationException;
using R1;

namespace L3.Runtime{
public class Process : Context{

    public L3Script root;
    public Env env;
    public History history = new ();
    public Record record;
    public object pose = new Logger();

    public Process(){}

    public Process(L3Script root) => this.root = root;

    public void Exec(){
        try{
            record = new ();
            Exec(root.value);
            history.Add(record);
        }catch(System.Exception){ env.Dump(); throw; }
    }

    public Node FindFunction(string name)
    => env.FindFunction(name) ?? FindFuncHere(name);

    public Node FindFuncHere(string name){
        var unit = root.value;
        foreach(var x in unit.children){
            var f = x as Function;
            if(f==null) continue;
            if(f.name == name) return f;
        }
        return null;
    }

    public object Step(Node exp, HashSet<Node> deps){
        var x = exp switch{
            Unit un => R1.Unit.Step(un, this, deps),
            _ => Step(exp)
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
        var x = new R1.Obj(clss);
        record.Exit(clss, value: x);
        return x;
    }

    void Exec(L3.Unit unit){
        Activ.Util.Types.SetCustomTypes(TypeMap.types);
        record.frame = null;
        if(env == null) env = new ();
        env.Enter();
        Step(unit);
        env.Exit();
    }

    // <Context> ----------------------------------------------

    Env Context.env => env;
    object Context.pose{ get => pose; set => pose = value; }
    History Context.history => history;

    class Logger{
        public void Log(object arg) => UnityEngine.Debug.Log(arg);
    }

}}
