using UnityEngine;
using System.Collections.Generic;
using InvOp = System.InvalidOperationException;

namespace L3{
public class Script : MonoBehaviour{

    public L3.Unit main;
    public Stack<Scope> stack;

    public Scope scope => stack.Peek();

    public void Update(){
        Log("----------");
        Step(main.func);
    }

    object Step(Node arg) => null;

    object Step(Function func){
        Log("fu/" + func);
        return Step(func.expression);
    }

    object Step(Expression exp){
        switch(exp){
            case Composite co: return Step(co);
            case Call      ca: return Invoke(ca);
            default: throw new InvOp($"Unknown {exp}");
        }
    }

    // TODO composites not implemented
    object Step(Composite co){
        Log("co/" + co);
        foreach(var k in co.nodes){
            Step(k);
        }
        return null;
    }

    object Invoke(Call ca){
        Log("ca/" + ca);
        // 1. Find the wanted function,
        var node = scope.Find(ca.name);
        // 2. Resolve args to sub-scope
        var sub = new Scope();
        foreach(var arg in ca.args){
            sub.Add(Step(arg) as Node);
        }
        // 3. Eval the function in-scope.
        stack.Push( sub );
        var output = Step(node);
        // 4. Exit sub-scope and return the output
        stack.Pop();
        return output;
    }

    void Log(object arg)
    => UnityEngine.Debug.Log(arg);

}}
