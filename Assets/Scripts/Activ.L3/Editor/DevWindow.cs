using UnityEngine; using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;
using GL = UnityEngine.GUILayout;
using L3;
using Self = L3.Editor.DevWindow;

namespace L3.Editor{
public class DevWindow : EditorWindow{

    public static Self instance;
    public L3Script target;
    string[] testReport;
    Vector2 scroll;
    GraphEd editor = new ();

    [MenuItem("Window/L3/Editor")]
    public static Self ShowWindow()
    => instance = GetWindow<Self>("L3 Editor");

    void OnGUI(){
        if(!hasTestReport) MainUI();
        else               DisplayTestReport();
    }

    void MainUI(){
        var prevTarget = target;
        BeginHorizontal();
        Button("New");
        if(target != null && Button("Run")){
            TestRunner.Run(target);
        }
        if(Button("Run tests")){
            testReport = TestRunner.RunTests();
        }
        EndHorizontal();
        Space(8);
        target = EGL.ObjectField(
            "Script", target, typeof(L3Script),
            allowSceneObjects: false
        ) as L3Script;
        if(target == null) return;
        if(target != prevTarget) NodeEditor.Edit(target.value);
        Space(8);
        editor.OnGUI(target);
    }

    void DisplayTestReport(){
        scroll = BeginScrollView(scroll);
        foreach(var k in testReport){
            var lines = k.Split("\n");
            foreach(var x in lines){
                var l = x;
                if(l.Trim().StartsWith("at")) l = CleanErrMsg(l);
                Label(l);
            }
        }
        EndScrollView();
        if(Button("OK", GL.Width(100))) testReport = null;
    }

    bool hasTestReport
    => testReport != null && testReport.Length > 0;

    string CleanErrMsg(string line){
        var i = line.IndexOf(" in ");
        var x = line.Substring(0, i + 4);
        var end = line.Substring(i);
        var j = line.LastIndexOf("/");
        end = line.Substring(j + 1);
        return x + end;
    }

    public static void Save(){
        if(instance == null) instance = ShowWindow();
        EditorUtility.SetDirty(Self.instance.target);
        instance.Repaint();
    }

}}
