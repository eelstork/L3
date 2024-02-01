using System.Reflection;
using L3; using Co = L3.Composite;
using static L3.Composite.Type;
using static L3.Token;
using InvOp = System.InvalidOperationException;
using UnityEngine;

namespace R1{
public static class Composite{

    public static object Ref(Co co, Context cx){
        var output = co.type switch{
            access => AccessRef(co, cx),
            //act => Act(co, cx),
            //assign => Assign(co, cx),
            //block => Block(co, cx),
            //sel => Sel(co, cx),
            //seq => Seq(co, cx),
            //sum => Sum(co, cx),
            _ => throw new InvOp($"Cannot ref: {co.type}")
        };
        return output;
    }

    // TODO BT style composites not implemented
    public static object Step(Co co, Context cx){
        var scoping = co.type != assign;
        if(scoping) cx.env.EnterScope();
        var output = co.type switch{
            access => Access(co, cx),
            act => Act(co, cx),
            assign => Assign(co, cx),
            block => Block(co, cx),
            sel => Sel(co, cx),
            seq => Seq(co, cx),
            sum => Sum(co, cx),
            _ => throw new InvOp($"Unknown composite: {co.type}")
        };
        if(scoping) cx.env.ExitScope();
        return output;
    }

    public static object Block(Co co, Context cx){
        cx.Log("blk/" + co);
        foreach(var k in co.nodes) cx.Step(k as Node);
        return null;
    }

    public static object AccessRef(Co co, Context cx){
        cx.Log("access-ref/" + co);
        var first = co.nodes[0] as Node;
        if(first == null){
            throw new InvOp($"{co.nodes[0]} is not a node");
        }
        var X = cx.Step(first);
        //ebug.Log($"Stepping {co.nodes[0]} yields {X}");
        for(var i = 1; i < co.nodes.Count; i++){
            var Y = co.nodes[i];
            switch (Y){
                case L3.Var @var:
                    // TODO
                    var X1 = X as R1.Obj;
                    if(X1 == null) throw new InvOp($"{X} is not an R1.Obj");
                    X = X1.Ref(@var, cx);
                    break;
                case L3.Call call:
                    X = R1.Call.Invoke(call, cx, X);
                    break;
                default:
                    throw new InvOp($"{Y} cannot access {X}");
            }
        }
        return X;
    }

    public static object Access(Co co, Context cx){
        cx.Log("access/" + co);
        var X = cx.Step(co.nodes[0] as Node);
        for(var i = 1; i < co.nodes.Count; i++){
            var Y = co.nodes[i];
            switch (Y){
                case L3.Var @var:
                    // TODO
                    X = (X as R1.Obj).Find(@var, cx);
                    break;
                case L3.Call call:
                    X = R1.Call.Invoke(call, cx, X);
                    break;
                default:
                    throw new InvOp($"{Y} cannot access {X}");
            }
        }
        return X;
    }

    public static object Assign(Co co, Context cx){
        cx.Log("assign/" + co);
        var n = co.nodes.Count;
        int i = n - 1;
        object val = cx.Step(co.nodes[i] as Node);
        for(i--; i >= 0; i--){
            var X = co.nodes[i];
            var X1 = X as Node;
            if(X1 == null){
                throw new InvOp($"{X} is not a Node");
            }
            var x = cx.Ref(X1);
            if(x == null){
                throw new InvOp($"Referring {X1} returned null");
            }else if(x is Assignable){
                Assign(val, (Assignable)x);
            }else{
                throw new InvOp($"{x} is not assignable");
            }
        }
        return val;
        void Assign(object value, Assignable @var){
            //UnityEngine.Debug.Log($"assign {value} to {@var}");
            @var.Assign(value);
        }
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

    public static object Sum(Co co, Context cx){
        cx.Log("seq/" + co);
        object x = null;
        foreach(var k in co.nodes){
            object y = cx.Step(k as Node);
            x = R1.Op.Sum.Add(x, y);
        }
        return x;
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
