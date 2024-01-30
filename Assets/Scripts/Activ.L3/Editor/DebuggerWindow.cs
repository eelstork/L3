using UnityEngine; using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;
using static Activ.Data.Traversal;
using L3;
using Frame = L3.Record.Frame;

namespace L3.Editor{
public class DebuggerWindow : EditorWindow{

    static DebuggerWindow instance;
    Record record;

    void OnGUI(){
        var obj = Selection.activeTransform;
        var script = obj.GetComponent<L3Component>();
        if(script == null){
            Label("Nothing to show");
        }else{
            Label($"{obj.name}/L3Component");
            record = script.record;
        }
        if(record.frame == null){
            Label("(no record)");
        }else{
            Traverse(record.frame, x => x.children, DrawNode);
        }
    }

    void OnInspectorUpdate() => Repaint();

    void DrawNode(Frame arg){
        Label(arg.ToString());
    }

    [MenuItem("Window/L3/Debugger")]
    public static DebuggerWindow ShowWindow()
    => instance = GetWindow<DebuggerWindow>("L3 Debugger");

}}
