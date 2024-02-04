using System; using System.Linq; using System.Reflection;
using Method = System.Reflection.MethodInfo;
using Constructor = System.Reflection.ConstructorInfo;
using InvOp = System.InvalidOperationException;
using static UnityEngine.Debug;

namespace L3{
public static class CSharp{

    public static object Construct(Type type, object[] args){
        var z = GetConstructors(type, args.Length);
        return Construct(z, args);
    }

    static Constructor[] GetConstructors(
        Type type, int paramcount
    )
    => type.GetConstructors()
           .Where(m => m.GetParameters().Length == paramcount)
           .ToArray();

    public static object Construct(
        Constructor[] group, object[] args
    ){
        foreach(var c in group){
            var output = c.Invoke(args);
            if(output != null){
                Log($"Value type [{output.GetType()}]");
            }
            return output;
        }
        throw new InvOp("Could not call method");
    }

    public static object Invoke(
        Method method, object[] args, object target
    ){
        Log($"CALLING {method.Name}");
        var rtype = method.ReturnType;
        var output = method.Invoke(target, args);
        if(output != null){
            Log($"Value type [{output.GetType()}]");
        }
        return output;
    }

    public static (Type type, object value) Invoke(
        Method[] group, object[] args, object target
    ){
        if(group.Length == 0) throw new InvOp("Empty method group");
        foreach(var method in group){
            //try{
                var output = method.Invoke(target, args);
                //if(output != null){
                //    Log($"Value type [{output.GetType()}]");
                //}
                return (method.ReturnType, output);
            //}catch()
        }
        throw new InvOp("Could not call method");
    }

}}
