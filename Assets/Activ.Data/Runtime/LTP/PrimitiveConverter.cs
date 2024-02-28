using InvOp = System.InvalidOperationException;
using v3 = UnityEngine.Vector3;
using q4 = UnityEngine.Quaternion;
using Activ.Util;

namespace Activ.LTP{
public static class PrimitiveConverter{

    public static string ToString(object arg){
        if(arg is string || arg.GetType().IsPrimitive)
            return arg.ToString();
        else return arg switch{
            v3 v => ToCSV(v),
            q4 q => ToCSV(q),
            _ => throw new InvOp($"Cannot convert {arg}")
        };
    }

    static string ToCSV(v3 u)
    => u.x + "," + u.y + "," + u.z;

    static string ToCSV(q4 q)
    => q.x + "," + q.y + "," + q.z + "," + q.w;

}}
