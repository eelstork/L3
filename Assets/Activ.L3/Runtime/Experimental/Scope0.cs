using System.Collections.Generic;
using InvOp = System.InvalidOperationException;
using System.Linq; using Activ.Util;

namespace Experimental{
public class Stack0 : Stack<Stack0.Scope>{

    public Stack0() => EnterFrame();

    public void EnterFrame() => Push( new Scope(clear: false) );
    public void ExitFrame () => Pop();
    public void EnterBlock() => Push( new Scope(clear: true) );
    public void ExitBlock () => Pop();

    public bool Exists(object key){
        foreach(var scope in this){
            if(scope.ContainsKey(key)) return true;
            if(!scope.clear) return false;
        } return false;
    }

    public object this[object key]{
        get{
            int i = 0;
            foreach(var scope in this){
                if(scope.ContainsKey(key)) return scope[key];
                if(!scope.clear) return false;
                i++;
            } throw new InvOp($"Not found: {key}");
        }
        set => Peek()[key] = value;
    }

    public class Scope : Dictionary<object, object>{
        public readonly bool clear;
        public Scope(bool clear) => this.clear = clear;
    }

    // --------------------------------------------------------------

    public string Dump(){
        var z = "stack\n";
        foreach(var scope in this.Reverse()){
            z += scope.clear ? "block\n" : "frame\n";
            foreach(var kv in scope)
                z += $"{kv.Key}: {kv.Value}\n".Tabs(1);
        } return z;
    }

}}
