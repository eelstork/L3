using System.Reflection;
using L3; using Co = L3.Composite;
using static L3.Composite.Type;
using static L3.Token;
using InvOp = System.InvalidOperationException;
using UnityEngine;

namespace R1{
public static class Composite{

    // TODO BT style composites not implemented
    public static object Step(Co co, Context cx){
        var scoping = co.type != assign;
        if(scoping) cx.env.PushBlock();
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
        if(scoping) cx.env.PopBlock();
        return output;
    }

    public static object Block(Co co, Context cx){
        cx.Log("blk/" + co);
        foreach(var k in co.nodes) cx.Step(k as Node);
        return null;
    }

    public static object Access(Co co, Context cx){
        cx.Log("access/" + co);
        object prev = null, val = null;
        foreach(var k in co.nodes){
            if(prev != null){
                if(prev is Accessible){
                    Debug.Log($"Access {prev}");
                    val = ((Accessible)prev).Find(k as Node, cx);
                    prev = val;
                }else{
                    throw new InvOp($"{prev} is not Accessible");
                }
            }else{
                val = cx.Step(k as Node);
                prev = val;
            }
        }
        return val;
    }

    public static object Assign(Co co, Context cx){
        cx.Log("assign/" + co);
        var n = co.nodes.Count;
        object val = null;
        for(int i = n - 1; i >= 0; i--){
            var next = cx.Step(co.nodes[i] as Node);
            if(val != null){
                if(next is Assignable){
                    Assign(val, (Assignable)next);
                }else{
                    throw new InvOp($"{next} is not assignable");
                }
            }
            val = next;
        }
        return val;
        void Assign(object value, Assignable @var){
            UnityEngine.Debug.Log($"assign {value} to {@var}");
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
