#if UNITY_EDITOR
using UnityEngine; using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;
using L3;

public class GraphEditor : EditorWindow{

    GUIStyle nodeStyle;
    public Unit target;
    public static GraphEditor instance;

    [MenuItem("Window/Graph Editor")]
    public static GraphEditor ShowWindow()
    => instance = GetWindow<GraphEditor>("L3 Graph");

    void OnGUI(){
        CreateStyles();
        target = EGL.ObjectField(
            "Unit", target, typeof(Unit), allowSceneObjects: false
        ) as L3.Unit;
        if(target == null) return;
        Draw(target.func);
    }

    void Draw(Node node, int depth=0){
        DrawNode(node, depth);
        switch(node){
            case Branch branch:
                if(branch.children == null) return;
                foreach(var child in branch.children){
                    Draw(child, depth + 1);
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

    void DrawNode(Node client, int tabs = 0){
        var label = client.TFormat();
        BeginHorizontal();
        Space(tabs * 8 * 4);
        if(Button(label, nodeStyle)){
            NodeEditor.Edit(client);
        }
        Button("+", Width(32));
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
