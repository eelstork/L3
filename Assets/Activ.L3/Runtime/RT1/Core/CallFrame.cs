using System.Collections.Generic;
using InvOp = System.InvalidOperationException;

namespace R1{
public class CallFrame : Stack<Scope>{

    R1.Obj @object; public object pose;

    public CallFrame(object pose)
    { this.pose = pose ?? new Logger(); Push( new () ); }

    public CallFrame(Scope arg){ pose = new Logger(); Push(arg); }

    public CallFrame(Scope arg, Obj target){
        pose = new Logger(); this.@object = target; Push(arg);
    }

    public object GetValue(string @var, bool opt){
        // 1. Search local scope
        foreach(var z in this){
            var node = z.FindValueHolder(@var);
            if(node != null) return node.value;
        }
        // 2. Search current object, if any
        if(@object != null && @object.map.ContainsKey(@var)){
            return @object.map[@var];
        }
        // 3. Search pose, if any
        if(pose != null){
            var @out = pose.GetFieldOrPropertyValue(
                @var, out bool found
            ); if(found) return @out;
        }
        // TODO false is confusing here
        if(opt) return false;
        throw new InvOp($"Variable not in scope: {@var}");
    }

    public object Reference(string @var, bool opt){
        // 1. Search local scope
        foreach(var z in this){
            var node = z.FindValueHolder(@var);
            if(node != null) return node;
        }
        // 2. Search current object, if any
        if(@object != null && @object.map.ContainsKey(@var)){
            return @object.Ref(@var);
        }
        // 3. Search pose, if any
        if(pose != null){
            var type = pose.GetType();
            var field = type.GetField(@var);
            if(field != null) return new FieldRef(field, pose);
            var prop = type.GetProperty(@var);
            if(prop != null) return new PropRef(prop, pose);
        }
        // TODO false is confusing here
        if(opt) return false;
        throw new InvOp($"Variable not in scope: {@var}");
    }

    // -------------------------------------------------------------

    class Logger{
        public void Log(object arg) => UnityEngine.Debug.Log(arg);
    }

}}
