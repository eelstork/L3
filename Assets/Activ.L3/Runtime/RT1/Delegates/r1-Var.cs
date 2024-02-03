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
        // TODO this is not correct... may be in env and still null
        if(node != null){
            switch(node){
                case Variable x: return x.value;
                case Arg      x: return x.value;
            }
            return node;
        }
        // 2. Find the wanted variable in native object scope
        var @out = cx.pose.GetFieldOrPropertyValue(name, out bool found);
        if(found) return @out;
        else if(@var.opt) return false;
        else throw new InvOp(
            $"No field or property matching {name}"
        );
    }

    public static object Refer(L3.Var @var, Context cx){
        cx.Log("var-ref/" + @var + "(refer)");
        var name = @var.value;
        // 1. Find the wanted variable in scope, if possible
        var node = cx.env.FindVar(name);
        cx.Log($"{@var} in env as {node}");
        // TODO this is not correct... may be in env and still null
        if(node != null){
            switch(node){
                case Variable x: return x;
                // TODO - test re-assigning an argument if wanted
                case Arg x: return x;
            }
            return node;
        }
        // 2. Find the wanted variable in native object scope
        var type = cx.pose.GetType();
        var field = type.GetField(name);
        // TODO "Object here is poorly named"
        if(field != null) return new FieldRef(field, cx.pose);
        var prop = type.GetProperty(name);
        if(prop != null) return new PropRef(prop, cx.pose);
        if(@var.opt){
            return false;
        }else throw new InvOp(
            $"No field or property matching {name}"
        );
    }

}}
