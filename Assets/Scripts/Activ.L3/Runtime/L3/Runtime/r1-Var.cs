using System.Reflection;
using InvOp = System.InvalidOperationException;
using L3;

namespace R1{
public static class Var{

    public static object Resolve(L3.Var @var, Context cx){
        cx.Log("var/" + @var + "(resolve)");
        var name = @var.value;
        // 1. Find the wanted variable in scope, if possible
        var node = cx.env.FindVar(name);
        cx.Log($"{@var} in env as {node}");
        if(node != null){
            switch(node){
                case Variable x:
                    //cx.Log($"return value of {x}, {x.value}");
                    return x.value;
                case Arg x:
                    return x.value;
            }
            return node;
        }else{
            cx.Log($"{name} not in env");
        }
        // 2. Find the wanted variable in native object scope
        var cs = ResolveCsVar(name, cx);
        if(cs != null) return new L3.Object(cs, cx);
        //
        var prop = cx.GetType().GetProperty(name);
        if(prop != null) return new L3.BoundProp(prop, cx);
        //
        if(@var.opt){
            return false;
        }else throw new InvOp(
            $"No field or property matching {name}"
        );
    }

    static FieldInfo ResolveCsVar(string name, Context context)
    => context.GetType().GetField(name);
    //?? context.GetType().GetProperty(name);

}}
