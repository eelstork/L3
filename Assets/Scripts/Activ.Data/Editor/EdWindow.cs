using UnityEngine; using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;

namespace Activ.Data{
public class EdWindow : EditorWindow{

    public UnityEngine.Object target;
    Ed ed = new ();
    public static EdWindow instance;

    [MenuItem("Window/Activ.Data")]
    public static EdWindow ShowWindow()
    => instance = GetWindow<EdWindow>("L3 Graph");

    void OnGUI(){
        target = EGL.ObjectField(
            "SO", target, typeof(UnityEngine.Object),
            allowSceneObjects: false
        );
        if(target == null) return;
        ed.OnGUI(target);
    }

}}
