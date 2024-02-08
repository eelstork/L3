using UnityEngine; using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;
using L3;

namespace L3.Editor{
public class L3NodeInspector : EditorWindow{

    Node selection;
    static L3NodeInspector instance;
    NodeEd inspector;

    [MenuItem("Window/L3/Node Editor")]
    public static L3NodeInspector ShowWindow()
    => instance = GetWindow<L3NodeInspector>("L3 Node Inspector");

    void OnGUI(){
        if(inspector == null) inspector = new NodeEd();
        if(selection == null){
            Label("No selection");
            return;
        }
        inspector.OnGUI(
            selection, out bool didUseAction, out bool didEdit
        );
        if(didUseAction || didEdit){
            DevWindow.Save();
        }
    }

    public static void Edit(Node arg){
        var i = ShowWindow();
        i.selection = arg;
    }

}}
