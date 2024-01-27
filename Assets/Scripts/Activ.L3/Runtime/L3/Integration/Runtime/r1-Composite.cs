using System.Reflection;
using L3; using Co = L3.Composite;
using static L3.Composite.Type;
using static L3.Token;
using InvOp = System.InvalidOperationException;

namespace R1{
public static class Composite{

    // TODO BT style composites not implemented
    public static object Step(Co co, Context cx)
    => co.type switch{
        block => Block(co, cx),
        sel => Sel(co, cx),
        seq => Seq(co, cx),
        act => Act(co, cx),
        _ => throw new InvOp($"Unknown composite: {co.type}")
    };

    public static object Block(Co co, Context cx){
        cx.Log("blk/" + co);
        foreach(var k in co.nodes) cx.Step(k as Node);
        return null;
    }

    public static object Sel(Co co, Context cx){
        cx.Log("sel/" + co);
        object val = null;
        foreach(var k in co.nodes){
            val = cx.Step(k as Node);
            if(val == (object)true) return val;
            if(val == (object)@cont) return val;
            if(val == (object)@void) return val;
        }
        return val;
    }

    public static object Seq(Co co, Context cx){
        cx.Log("seq/" + co);
        object val = null;
        foreach(var k in co.nodes){
            val = cx.Step(k as Node);
            if(val == (object)false) return val;
            if(val == (object)@cont) return val;
        }
        return val;
    }

    public static object Act(Co co, Context cx){
        cx.Log("act/" + co);
        object val = null;
        foreach(var k in co.nodes){
            val = cx.Step(k as Node);
            if(val == (object)@cont) return val;
        }
        return val;
    }

}}
