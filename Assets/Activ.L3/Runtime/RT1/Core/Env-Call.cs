using System.Collections.Generic;
using InvOp = System.InvalidOperationException;
using Method = System.Reflection.MethodInfo;
using Activ.Util; using L3;

namespace R1{
public partial class Env{

    // TODO must verify that upstream does not emit null targets,
    // as this would result in undefined behavior
    // - Only examine the target, if available
    // - Otherwise resolve against local scope, then the pose
    public object CallFunction(
        object target, string name, object[] args, Context cx
    ) => target switch{
        Obj    l3obj => CallL3TargetFunc(l3obj, name, args, cx),
        object csobj => CallCsTargetFunc(csobj, name, args),
        null         => CallLocalFunc(name, args, cx)
    };

    public object CallL3TargetFunc(
        R1.Obj target, string name, object[] args, Context cx
    ){
        var l3func = target.map[name] as L3.Function;
        return CallL3Func(target, l3func, args, cx);
    }

    object CallCsTargetFunc(
        object target, string name, object[] args
    ){
        var type = target.GetType();
        var group = type.FindMethodGroup(name, target, args.Length);
        return CallCsFunc(target, group, args);
    }

    object CallLocalFunc(
        string name, object[] args, Context cx
    ){
        object target = frame.pose;
        var l3func = frame.FindL3Func(target, name, args);
        if(l3func != null) return CallL3Func(
            target, l3func, args, cx
        );
        var csfunc = frame.FindCsFunc(target, name, args);
        if(csfunc != null) return CallCsFunc(
            target, csfunc, args
        );
        throw new InvOp("Function not found: " + name);
    }

    public object CallL3Func(
        object target, Function func, object[] args, Context cx
    ){
        var sub = new Scope();
        // Load arguments into the subscope
        for(int i = 0; i < args.Length; i++){
            var arg = new Arg(func.parameters[i].name, args[i]);
            sub.Add(arg);
        }
        EnterCall(sub, target);
        //Debug.Log($"CALL simple function: [{node}]");
        var content = func.expression as Node;
        object output;
        if(content != null){
            output = cx.Step(content);
        }else{
            if(func.auto){
                return Solver.Find(func.type, args);
            }
            output = Token.@void;
        }
        // Exit subscope and return the output
        ExitCall();
        return output;
    }

    object CallCsFunc(object target, Method[] group, object[] args){
        var output = CSharp.Invoke(group, args, target);
        if(output.type.Equals(typeof(void))){
            return Token.@void;
        }else{
            return output.value;
        }
    }

}}
