using System.Reflection;
using L3;

namespace R1{
public static class Field{

    public static object Step(L3.Field field, Context cx){
        cx.Log("u/" + field);
        var x = new Variable(field);
        cx.scope.Add(x);
        return x;
    }

}}
