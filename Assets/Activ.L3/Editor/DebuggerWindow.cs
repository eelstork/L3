using System.Collections.Generic;
using UnityEngine; using static UnityEngine.GUILayout;
using UnityEditor; using EGL = UnityEditor.EditorGUILayout;
using static Activ.Data.Traversal;
using Frame = R1.Record.Frame;
using Record = R1.Record;
using History = R1.History;

namespace L3.Editor{
public class DebuggerWindow : EditorWindow{

    static DebuggerWindow instance; Record record;
    public static R1.L3TestEnv testResult;
    Vector2 scroll;

    void OnGUI(){
        var history = GetHistory();
        var record = history?.last;
        if(record == null || record.frame == null){
            Label("(no record)");
        }else{
            scroll = BeginScrollView(scroll);
            Traverse(record.frame, x => x.children, DrawNode);
            EndScrollView();
            Space(8);
            var n = history.count;
            Label($"{n}/{n} - {record.date.ToString()}");
        }
    }

    History GetHistory(){
        History h;
        var obj = Selection.activeTransform;
        var script = obj ? obj.GetComponent<L3Component>() : null;
        if(script != null){
            h = script.history;
            if(h != null) return h;
        }
        if(testResult == null) return null;
        return testResult.history;
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
