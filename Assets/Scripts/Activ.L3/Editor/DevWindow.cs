using UnityEngine; using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;
using L3;
using Self = L3.Editor.DevWindow;

namespace L3.Editor{
public class DevWindow : EditorWindow{

    public static Self instance;
    public L3Script target;
    GraphEd editor = new ();

    [MenuItem("Window/L3/Editor")]
    public static Self ShowWindow()
    => instance = GetWindow<Self>("L3 Editor");

    void OnGUI(){
        var prevTarget = target;
        BeginHorizontal();
        Button("New"); Button("Run");
        if(Button("Run tests")){
            TestRunner.RunTests();
        }
        EndHorizontal();
        Space(8);
        target = EGL.ObjectField(
            "Script", target, typeof(L3Script), allowSceneObjects: false
        ) as L3Script;
        if(target == null) return;
        if(target != prevTarget) NodeEditor.Edit(target.value);
        Space(8);
        editor.OnGUI(target);
    }

    public static void Save(){
        if(instance == null) instance = ShowWindow();
        EditorUtility.SetDirty(Self.instance.target);
        instance.Repaint();
    }

}}
