using System.Reflection;
using InvOp = System.InvalidOperationException;
using L3;

namespace R1{
public static class Var{

    public static object Resolve(L3.Var @var, Context cx){
        cx.Log("var/" + @var);
        var name = @var.value;
        // 1. Find the wanted variable in scope, if possible
        var node = cx.scope?.Find(name);
        if(node != null) return node;
        // 2. Find the wanted variable in native object scope
        FieldInfo cs = null;
        cs = ResolveCsVar(name, cx);
        if(cs == null){
            if(@var.opt){
                return false;
            }else throw new InvOp(
                $"No C# field or property matching {name}"
            );
        }
        var value = cs.GetValue(cx);
        UnityEngine.Debug.Log($"Var returns {value}");
        return new L3.Object(value);
    }

    static FieldInfo ResolveCsVar(string name, Context context)
    => context.GetType().GetField(name);
    //?? context.GetType().GetProperty(name);

}}
