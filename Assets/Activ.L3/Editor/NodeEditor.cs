using UnityEngine; using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;
using L3;

namespace L3.Editor{
public class NodeEditor : EditorWindow{

    Node selection;
    static NodeEditor instance;
    ObjectInspector inspector;

    [MenuItem("Window/L3/Node Editor")]
    public static NodeEditor ShowWindow()
    => instance = GetWindow<NodeEditor>("L3 Node Inspector");

    void OnGUI(){
        if(inspector == null) inspector = new ObjectInspector();
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
