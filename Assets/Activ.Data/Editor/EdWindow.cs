using UnityEngine; using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;
using Self = Activ.Data.EdWindow;

namespace Activ.Data{
public class EdWindow : EditorWindow{

    public UnityEngine.Object target;
    Ed ed = new ();
    public static Self instance;

    [MenuItem("Window/Activ.Data")]
    public static Self ShowWindow()
    => instance = GetWindow<Self>("L3 Graph");

    void OnGUI(){
        target = EGL.ObjectField(
            "SO", target, typeof(UnityEngine.Object),
            allowSceneObjects: false
        );
        if(target == null) return;
        ed.OnGUI(target);
    }

}}
