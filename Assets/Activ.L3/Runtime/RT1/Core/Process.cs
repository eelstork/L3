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
    public object _pose;
    public object output;
    public bool stopped;

    public Process(){}

    public Process(L3Script root) => this.root = root;

    public object pose => env.pose;

    public object Exec(){
        if(stopped) return "stopped";
        try{
            record = new ();
            var value = Exec(root.value);
            history.Add(record);
            return value;
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
                Unit      un => RunUnit(un),
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

    object RunUnit(L3.Unit un){
        output = R1.Unit.Step(un, this, new ());
        if(L3.Statuses.IsDone(output)){
            if(un.once) stopped = true;
        }
        return output;
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

    object Exec(L3.Unit unit){
        Activ.Util.Types.SetCustomTypes(TypeMap.types);
        record.frame = null;
        if(env == null) env = new ();
        env.Enter(_pose);
        var value = Step(unit);
        env.Exit();
        return value;
    }

    Dictionary<Composite, int> state = new ();

    public void SetIndex(Composite arg, int i){
        state[arg] = i;
    }

    public int GetIndex(Composite arg){
        if(state.ContainsKey(arg)) return state[arg];
        else return 0;
    }

    // <Context> ----------------------------------------------

    Env Context.env => env;

    object Context.pose{
        get => env.pose;
        set{ _pose = value; if(env != null) env.pose = value; }
    }

    History Context.history => history;

}}
