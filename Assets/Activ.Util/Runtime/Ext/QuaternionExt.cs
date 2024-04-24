using UnityEngine;
using q4 = UnityEngine.Quaternion;
using InvOp = System.InvalidOperationException;
using Activ.Util;

public static class QuaternionExt{

    public static q4 FromCSV(string arg){
        var v = arg.Split(',');
        return new q4(
            float.Parse(v[0]), float.Parse(v[1]), float.Parse(v[2]),
            float.Parse(v[3])
        );
    }

    public static string ToCSV(this q4 q)
    => q.x + "," + q.y + "," + q.z + "," + q.w;

    public static float CSum(this q4 q)
    => q.x + q.y + q.z + q.w;

}
