using System.Collections.Generic;
using InvOp = System.InvalidOperationException;
using Method = System.Reflection.MethodInfo;
using Activ.Util;

namespace R1{
public class CallFrame : Stack<Scope>{

    R1.Obj @object; public object pose;

    public CallFrame(object pose)
    { this.pose = pose ?? new Logger(); Push( new () ); }

    // -------------------------------------------------------------

    public CallFrame(Scope arg){ pose = new Logger(); Push(arg); }

    public CallFrame(Scope arg, Obj target){
        pose = new Logger(); this.@object = target; Push(arg);
    }

    // -------------------------------------------------------------

    public L3.Function FindL3Func(
        object target, string name, object[] args
    ){
        // TODO we should not search scope if a target is specified
        // 1. Search local scope
        foreach(var scope in this){
            // TODO args must be accounted for
            var l3func = scope.FindFunction(name);
            if(l3func != null) return l3func;
        }
        // 2. Search current object, if any
        if(@object != null && @object.map.ContainsKey(name)){
            return @object.map[name] as L3.Function;
        }
        return null;
    }

    public Method[] FindCsFunc(
        object target, string name, object[] args
    ){
        if(pose != null){
            var type = pose.GetType();
            return type.FindMethodGroup(name, target, args.Length);
        }else return null;
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
