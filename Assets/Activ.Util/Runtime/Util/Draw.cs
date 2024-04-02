using System.Collections.Generic;
using UnityEngine;
using v3 = UnityEngine.Vector3;

namespace Activ.Util{
public static class Draw{

    public static void Path(
        IEnumerable<v3> path, Color col, float offset = 0.01f
    ){
        v3? P = null; foreach(var Q in path){
            if(P.HasValue) Debug.DrawLine(
                P.Value + v3.up * offset, Q, col
            ); P = Q;
        }
    }

}}
