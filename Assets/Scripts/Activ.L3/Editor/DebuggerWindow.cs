using System.Collections.Generic;
using UnityEngine; using static UnityEngine.GUILayout;
using UnityEditor; using EGL = UnityEditor.EditorGUILayout;
using static Activ.Data.Traversal;
using Frame = L3.Record.Frame;

namespace L3.Editor{
public class DebuggerWindow : EditorWindow{

    static DebuggerWindow instance; Record record;
    public static L3TestEnv testResult;

    void OnGUI(){
        var record = GetRecord();
        if(record == null || record.frame == null){
            Label("(no record)");
        }else{
            Traverse(record.frame, x => x.children, DrawNode);
        }
    }

    Record GetRecord(){
        Record record;
        var obj = Selection.activeTransform;
        var script = obj ? obj.GetComponent<L3Component>() : null;
        if(script != null){
            record = script.record;
            if(record != null) return record;
        }
        if(testResult == null) return null;
        return testResult.record;
    }

    void OnInspectorUpdate() => Repaint();

    void DrawNode(Frame arg){
        //if(arg.node is L3.Dec && arg.error == null) return;
        BeginHorizontal();
        Space(arg.depth * 16); Label(arg.ToString());
        EndHorizontal();
    }

    [MenuItem("Window/L3/Debugger")]
    public static DebuggerWindow ShowWindow()
    => instance = GetWindow<DebuggerWindow>("L3 Debugger");

}}
