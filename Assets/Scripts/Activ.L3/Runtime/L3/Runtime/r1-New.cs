using System.Reflection;
using InvOp = System.InvalidOperationException;
using L3;
using System.Linq;
using UnityEngine;
using Types = Activ.Util.Types;

namespace R1{
public static class New{

    // NOTE - target may be null
    public static object Invoke(
        L3.New nw, Context cx, object target
    ){
        cx.Log("construct/" + nw);
        var name = nw.type;
        // Find the wanted function,
        var cstype = Types.Find(name);
        // Resolve args to sub-scope
        //var sub = new Scope();
        //foreach(var arg in nw.args){
        //    sub.Add(cx.Step(arg as Node) as Node);
        //}
        var args = new object[nw.args.Count];
        for(var i = 0; i < args.Length; i++){
            args[i] = cx.Step(nw.args[i] as Node);
        }
        if(cstype != null){  // (C#) native call
            return CSharp.Construct(cstype, args);
        }else{                            // L3 call
            var node = cx.env.FindConstructor(name);
            if(node == null) throw new InvOp($"No constructor for {nw.type}");
            // Push the subscope
            var output = cx.Instantiate(node as Class);
            //cx.env.Push( sub, output );
            //Debug.Log($"CALL l3 constructor: [{node}]");
            // Exit subscope and return the output
            //cx.env.Pop();
            cx.Log($"Created custom type instance: {output}");
            return output;
        }
    }

}}
