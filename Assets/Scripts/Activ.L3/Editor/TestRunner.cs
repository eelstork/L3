using static UnityEngine.Debug;
using Activ.Util;
using L3;
using System.Linq;

namespace L3.Editor{
public static class TestRunner{

    public static void RunTests(){
        var tests = Assets.FindAll<L3Script>()
                          .Where(x => x.value.isTest);
        foreach(var k in tests){
            Log($"SHOULD RUN TEST: {k}");
        }
    }

}}
