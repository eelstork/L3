using UnityEngine;
using static UnityEngine.Debug;
using InvOp = System.InvalidOperationException;
using L3;
using R1;

namespace R1.Op{
public static class Sum{

    public static object Add(object a, object b){
        if(a == null) return b;
        a = Unwrap(a);
        b = Unwrap(b);
        //Debug.Log($"{a}, {a.GetType()}, vec3: {a is Vector3}");
        //Debug.Log($"{b}, {b.GetType()}, int: {b is int}");
        if((a is Vector3) && (b is float)){
            return Add((Vector3)a, (float)b);
        }
        throw new InvOp($"Add is not implemented for {a} + {b}");
    }

    static object Add(Vector3 A, float b){
        Log("Add vec + int");
        Debug.Break();
        return new Vector3(A.x + b, A.y + b, A.z + b);
    }

    static object Unwrap(object arg) => arg switch{
        FieldRef x => x.value,
        PropRef p => p.value,
        Literal l => l.value,
        //Vector3 v => v,
        //int n => n,
        _ => throw new InvOp("Cannot unwrap " + arg)
    };

}}
