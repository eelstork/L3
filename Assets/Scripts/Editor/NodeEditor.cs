using UnityEngine; using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;
using L3;

public class NodeEditor : EditorWindow{

    Node selection;
    static NodeEditor instance;
    ObjectInspector inspector;

    [MenuItem("Window/Node Editor")]
    public static NodeEditor ShowWindow()
    => instance = GetWindow<NodeEditor>("L3 Node Inspector");

    void OnGUI(){
        if(inspector == null) inspector = new ObjectInspector();
        Edits.nodeEditor = this;
        if(selection == null){
            Label("No selection");
            return;
        }
        inspector.OnGUI(
            selection,
            out bool didUseAction,
            out bool didEdit
        );
        //selection.OnGUI();
        if(Edits.didChange || didUseAction || didEdit){
            GraphEditor.Save();
        }
    }

    public static void Edit(Node arg){
        var i = ShowWindow();
        i.selection = arg;
    }

    //public static void Save(){
        //if(GraphEditor.instance.)
        //EditorUtility.SetDirty(GraphEditor.instance.target);
        //GraphEditor.DoRepaint();
        //Edits.didChange = false;
    //}

}
