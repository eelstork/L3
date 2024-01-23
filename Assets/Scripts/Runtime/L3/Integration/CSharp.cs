using Method = System.Reflection.MethodInfo;

namespace L3{
public static class CSharp{

    public static object Invoke(Method method, Scope args, Script cx){
        var _args = args.Unwrap();
        return method.Invoke(cx, _args);
    }

}}
