#if UNITY_EDITOR
using UnityEngine; using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;
using L3;

public class GraphEditor : EditorWindow{

    GUIStyle nodeStyle;
    public L3Script target;
    public static GraphEditor instance;

    [MenuItem("Window/L3/Graph Editor")]
    public static GraphEditor ShowWindow()
    => instance = GetWindow<GraphEditor>("L3 Graph");

    void OnGUI(){
        CreateStyles();
        var prevTarget = target;
        target = EGL.ObjectField(
            "Script", target, typeof(L3Script), allowSceneObjects: false
        ) as L3Script;
        if(target == null) return;
        if(target != prevTarget) NodeEditor.Edit(target.value);
        Draw(target.value, prefix: null, depth: 0, out bool _);
    }

    void Draw(Node node, string prefix, int depth, out bool del){
        DrawNode(node, prefix, depth, out del);
        switch(node){
            case Branch branch:
                var children = branch.children;
                if(children == null) return;
                prefix = branch.childPrefix;
                Node toDelete = null;
                for(var i = 0; i < children.Length; i++){
                    var child = children[i];
                    Draw(
                        child,
                        i == 0 ? null : branch.childPrefix,
                        depth + 1,
                        out bool del1
                    );
                    if(del1) toDelete = child;
                }
                if(toDelete != null){
                    branch.DeleteChild(toDelete);
                    EditorUtility.SetDirty(target);
                }
                break;
            default: return;
        }
    }

    public static void Save(){
        if(instance == null) instance = ShowWindow();
        EditorUtility.SetDirty(GraphEditor.instance.target);
        instance.Repaint();
    }

    static Texture2D tex;

    void DrawNode(Node client, string prefix, int tabs, out bool del){
        //Debug.Log($"Draw {client}");
        var label = prefix + client.TFormat();
        BeginHorizontal();
        Space(tabs * 8 * 4);
        if(Button(label, nodeStyle)){
            NodeEditor.Edit(client);
        }
        if(tabs > 0){
            Button("↑", Width(20));
            Button("↓", Width(20));
            del = Button("x", Width(20));
        }else{
            del = false;
        }
        EndHorizontal();
    }

    void CreateStyles(){
        //if(nodeStyle != null) return;
        tex = MakeTex(new Color(0.1f, 0.1f, 0.1f, 0.3f));
        var s = new GUIStyle(GUI.skin.button);
        s.border = new RectOffset(0, 0, 0, 0);
        s.alignment = TextAnchor.MiddleLeft;
        s.normal.background = tex;
        s.normal.scaledBackgrounds = new Texture2D[]{ tex };
        s.onActive.background = tex;
        s.onFocused.background = tex;
        s.onHover.background = tex;
        s.onNormal.background = tex;
        nodeStyle = s;
    }

    Texture2D MakeTex(Color col){
        var tex = new Texture2D(10, 10);
        var pixels = new Color[100];
        for(int i = 0; i < 100; i++) pixels[i] = col;
        tex.SetPixels(pixels); tex.Apply(); return tex;
    }

}
#endif
