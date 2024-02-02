using System; using System.Collections.Generic; using System.Linq;
using static UnityEngine.Debug;
using Activ.Util; using L3;

namespace L3.Editor{
public static class TestRunner{

    public static void Run(L3Script arg){
        var env = new L3TestEnv(arg);
        DebuggerWindow.testResult = env;
        env.Exec();
    }

    public static string[] RunTests(){
        var tests = Assets.FindAll<L3Script>()
                          .Where(x => x.value.isTest);
        var report = new List<string>();
        foreach(var k in tests){
            var rep = k.name;
            try{
                var env = new L3TestEnv(k);
                env.Exec();
                if(ExpectsError(k)){
                    report.Add($"{rep} - failed; expected {k.value.expectedError}");
                }else{
                    report.Add($"{rep} - ok");                    
                }
            }catch(Exception e){
                if(k.value.expectedError == e.Message){
                    report.Add($"{rep} - ok");
                }else{
                    report.Add($"{rep} - failed \n{e}");
                }
            }
        }
        return report.ToArray();
    }

    static bool ExpectsError(L3Script k){
        var err = k.value.expectedError;
        return !string.IsNullOrEmpty(err);
    }

}}
