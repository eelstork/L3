using Method = System.Reflection.MethodInfo;
using static UnityEngine.Debug;
using InvOp = System.InvalidOperationException;
using System.Reflection;

namespace L3{
public static class CSharp{

    public static object Construct(
        ConstructorInfo[] group, object[] args, object target
    ){
        //Log($"CALLING {cst.Name}");
        //Log($"CALLING {method.Name}");
        //var _args = args.Unwrap();
        //int i = 0; foreach(var k in _args){
        //    Log($"Arg {i}: {_args[i]}");
        //    i++;
        //}
        //var rtype = method.ReturnType;
        foreach(var c in group){
            //try{
                var output = c.Invoke(target, args);
                if(output != null){
                    Log($"Value type [{output.GetType()}]");
                }
                return output;
            //}catch()
        }
        throw new InvOp("Could not call method");
    }

    public static object Invoke(
        Method method, object[] args, object target
    ){
        Log($"CALLING {method.Name}");
        //var _args = args.Unwrap();
        //int i = 0; foreach(var k in _args){
        //    Log($"Arg {i}: {_args[i]}");
        //    i++;
        //}
        var rtype = method.ReturnType;
        var output = method.Invoke(target, args);
        if(output != null){
            Log($"Value type [{output.GetType()}]");
        }
        return output;
    }

    public static object Invoke(
        Method[] group, object[] args, object target
    ){
        //Log($"CALLING {method.Name}");
        //var _args = args.Unwrap();
        //int i = 0; foreach(var k in _args){
        //    Log($"Arg {i}: {_args[i]}");
        //    i++;
        //}
        //var rtype = method.ReturnType;
        foreach(var method in group){
            //try{
                var output = method.Invoke(target, args);
                if(output != null){
                    Log($"Value type [{output.GetType()}]");
                }
                return output;
            //}catch()
        }
        throw new InvOp("Could not call method");
    }

}}
