using System.Collections.Generic;
using InvOp = System.InvalidOperationException;
using System.Linq;
using Activ.Util;

namespace Experimental{
public class Stack : Stack<Stack.Scope>{

    Map map;

    public Stack(Map map=null){ this.map = map; EnterFrame(); }

    public void EnterFrame(object target=null)
    => Push( new(target, clear: false));

    public void ExitFrame()
    => Pop();

    public void EnterBlock()
    => Push(new (null, clear: true));

    public void ExitBlock()
    => Pop();

    public bool Exists(object key)
    => Find(key, lenient: true).found;

    public object this[object key]{
        get => Find(key, lenient: false).@out;
        set => Peek()[key] = value;
    }

    (object @out, bool found) Find(object key, bool lenient){
        foreach(var scope in this){
            if(scope.ContainsKey(key)) return (scope[key], true);
            if(map != null){
                var z = map.Find(scope.target, key, out bool found);
                if(found) return (z, true);
            }
            if(!scope.clear) break;
        } return lenient ? (null, false)
                         : throw new InvOp($"Not found: {key}");
    }

    public class Scope : Dictionary<object, object>{
        public readonly bool clear;
        public readonly object target;
        public Scope(object target, bool clear){
            this.clear = clear;
            this.target = target;
        }
        override public string ToString(){
            var str = clear ? "block" : "frame";
            if(target != null) str += '(' + target.ToString() + ')';
            return str;
        }
    }

    // --------------------------------------------------------------

    public string Dump(){
        var z = "stack\n";
        foreach(var s in this.Reverse()){
            z += s.ToString() + '\n';
            foreach(var w in s) z += $"{w.Key}: {w.Value}\n".Tabs(1);
        } return z;
    }

}}
