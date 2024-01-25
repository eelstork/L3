using Method = System.Reflection.MethodInfo;
using static UnityEngine.Debug;

namespace L3{
public static class CSharp{

    public static object Invoke(Method method, Scope args, Script cx){
        Log($"CALLING {method.Name}");
        var _args = args.Unwrap();
        int i = 0; foreach(var k in _args){
            Log($"Arg {i}: {_args[i]}");
            i++;
        }
        var rtype = method.ReturnType;
        var output = method.Invoke(cx, _args);
        if(output != null){
            Log($"Value type [{output.GetType()}]");
        }
        return output;
    }

}}
