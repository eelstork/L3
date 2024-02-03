using UnityEngine; using L3.Runtime;

namespace L3{
[ExecuteInEditMode]
public class L3Component : MonoBehaviour{

    public L3.L3Script main;
    public bool onDemand = true, runOnce;
    Process proc;

    public R1.History history => proc?.history;
    public R1.Record record => proc?.record;

    public void Log(object arg) => UnityEngine.Debug.Log(arg);

    void OnEnable(){
        Activ.Util.Types.SetCustomTypes(TypeMap.types);
        proc = new Process();
    }

    void Update(){
        if(willRun) Run(main.value);
        runOnce = false;
    }

    void Run(Unit unit){
        proc.root = main;
        proc.pose = this;
        proc.Exec();
    }

    bool willRun => !onDemand || runOnce;

}}
