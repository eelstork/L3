using InvOp = System.InvalidOperationException;
using Random = UnityEngine.Random;
using System.Collections.Generic;

namespace Activ.Util{
public static class ArrayExt{

    // In one example, we are setup with probs 0.1, 0.5, 0.2
    // adding up to 0.8.
    // We draw x = 0.7
    // 0.7 < 0.1 ? false, continue with x = 0.6
    // 0.6 < 0.5 ? false, continue with x = 0.1
    // 0.1 < 0.2 ? true, take action 3
    public static void ChanceAction(
        this (float chance, System.Action act)[] self){
        var x = Random.value;
        foreach(var y in self){
            if(x < y.chance){ y.act(); return; }else{
                x -= y.chance; if(x < 0) throw new InvOp(
                    "Probs must add up to one or less"
                );
            }
        }
    }

}}
