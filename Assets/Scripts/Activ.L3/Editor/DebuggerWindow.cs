using System.Collections.Generic;
using UnityEngine; using static UnityEngine.GUILayout;
using UnityEditor; using EGL = UnityEditor.EditorGUILayout;
using static Activ.Data.Traversal;
using Frame = L3.Record.Frame;

namespace L3.Editor{
public class DebuggerWindow : EditorWindow{

    static DebuggerWindow instance; Record record;

    void OnGUI(){
        var obj = Selection.activeTransform;
        var script = obj.GetComponent<L3Component>();
        if(script == null){ Label("Nothing to show"); }else{
            Label($"{obj.name}/L3Component"); record = script.record;
        }
        if(record.frame == null){ Label("(no record)"); }else{
            Traverse(record.frame, x => x.children, DrawNode);
        }
    }

    void OnInspectorUpdate() => Repaint();

    void DrawNode(Frame arg){
        BeginHorizontal();
        Space(arg.depth * 16); Label(arg.ToString());
        EndHorizontal();
    }

    [MenuItem("Window/L3/Debugger")]
    public static DebuggerWindow ShowWindow()
    => instance = GetWindow<DebuggerWindow>("L3 Debugger");

}}
