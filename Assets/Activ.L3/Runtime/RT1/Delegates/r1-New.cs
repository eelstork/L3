using System.Collections.Generic; using System.Linq;
using System.Reflection;
using InvOp = System.InvalidOperationException;
using UnityEngine;
using Types = Activ.Util.Types;
using L3;

namespace R1{
public static class New{

    public static object Invoke(L3.New nw, Context cx){
        cx.Log("construct/" + nw);
        return NewObject(nw, cx)
            ?? NewCsObject(nw, cx)
            ?? throw new InvOp($"Cannot construct {nw.type}");
    }

    public static object NewObject(L3.New nw, Context cx){
        var name = nw.type;
        // TODO more likely, this is retrieving the class def...
        var node = cx.env.FindConstructor(name);
        if(node == null) return null;
        // TODO constructors not supported now...
        return cx.Instantiate(node as Class);
    }

    public static object NewCsObject(L3.New nw, Context cx){
        var type = Types.Find(nw.type);
        if(type == null) return null;
        return CSharp.Construct(type, EvalArgs(nw.args, cx));
    }

    public static object[] EvalArgs(
        List<Expression> argnodes, Context cx
    ) => (from x in argnodes select cx.Step(x as Node)).ToArray();

}}
