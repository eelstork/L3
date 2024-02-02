using UnityEngine; using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;
using L3;

namespace L3.Editor{
public class GraphEd{

    GUIStyle nodeStyle;
    Vector3 scroll;
    L3Script target;

    public void OnGUI(L3Script target){
        this.target = target;
        CreateStyles();
        scroll = EGL.BeginScrollView(scroll);
        Draw(target.value, prefix: null, depth: 0, out bool _);
        EGL.EndScrollView();
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
                        depth + 1, out bool del1
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

    static Texture2D tex;

    void DrawNode(Node client, string prefix, int tabs, out bool del){
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

}}
