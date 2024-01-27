using System.Reflection;
using L3;

namespace R1{
public static class Func{

    public static object Step(L3.Function func, Context cx){
        cx.Log("fu/" + func);
        cx.scope.Add(func);
        return null;
    }

}}
