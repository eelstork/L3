using System.Collections.Generic;
using UnityEngine; using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;
using Activ.Util;
using L3;
using static L3.Composite.Type;

namespace L3.Editor{
public class GraphEd{

    GUIStyle nodeStyle, expButtonStyle, activeNodeStyle;
    Vector3 scroll;
    L3Script target;
    Node current;

    public void OnGUI(L3Script target){
        this.target = target;
        CreateStyles();
        scroll = EGL.BeginScrollView(scroll);
        Draw(target.value, prefix: null, depth: 0, out bool _);
        EGL.EndScrollView();
    }

    bool IsExpanded(Node arg) => arg switch{
        Branch br => expanded.GetValue(br, DefaultState(br)),
        _ => false
    };

    Dictionary<Branch, bool> expanded = new ();

    void Draw(Node node, string prefix, int depth, out bool del){
        DrawNode(node, prefix, depth, out del);
        switch(node){
            case Branch br:
                if(!expanded.GetValue(br, DefaultState(br))) return;
                var children = br.children;
                if(children == null) return;
                prefix = br.childPrefix;
                Node toDelete = null;
                for(var i = 0; i < children.Length; i++){
                    var child = children[i];
                    child.SetParent(br);
                    Draw(
                        child,
                        i == 0 ? null : br.childPrefix,
                        depth + 1, out bool del1
                    );
                    if(del1) toDelete = child;
                }
                if(toDelete != null){
                    br.DeleteChild(toDelete);
                    EditorUtility.SetDirty(target);
                }
                break;
            default: return;
        }
    }

    static Texture2D tex, hoverTex;

    void DrawNode(Node client, string prefix, int tabs, out bool del){
        var label = prefix + client.TFormat(IsExpanded(client));
        BeginHorizontal();
        Space(tabs * 8 * 4);
        if(client is Branch){
            var br = client as Branch;
            var exp = expanded.GetValue(br, DefaultState(br));
            if( Button( exp ? "-" : "+", expButtonStyle, Width(20)) ){
                expanded[br] = !exp;
            }
        }else{
            Button(" ", expButtonStyle, Width(20));
        }
        if(Button(label,
                  client == current ? activeNodeStyle : nodeStyle)
                 ) NodeEditor.Edit(current = client);
        if(tabs > 0){
            Button("↑", Width(20));
            Button("↓", Width(20));
            del = Button("x", Width(20));
        }else{
            del = false;
        }
        EndHorizontal();
    }

    bool DefaultState(Branch br) => br switch{
        Class => true,
        Composite x when x.type == block => true,
        Composite x when x.type == sel => true,
        Composite x when x.type == seq => true,
        Composite x when x.type == act => true,
        Unit => true,
        _ => false
    };

    void CreateStyles(){
        //if(nodeStyle != null) return;
        var hover = Color.yellow;
        expButtonStyle = MakeStyle(
            new Color(0.1f, 0.1f, 0.1f, 0.1f), Color.gray, hover
        );
        nodeStyle = MakeStyle(
            new Color(0.1f, 0.1f, 0.1f, 0.2f), null, hover
        );
        activeNodeStyle = MakeStyle(
            new Color(0.3f, 0.1f, 0.1f, 0.5f), null, hover
        );
    }

    GUIStyle MakeStyle(
        Color bg, Color? textColor = null, Color? hoverCol = null
    ){
        tex = MakeTex(bg);
        if(hoverCol != null){
            hoverTex = MakeTex(hoverCol.Value);
        }else hoverTex = tex;
        var s = new GUIStyle(GUI.skin.button);
        s.border = new RectOffset(0, 0, 0, 0);
        s.alignment = TextAnchor.MiddleLeft;
        s.normal.background = tex;
        s.normal.scaledBackgrounds = new Texture2D[]{ tex };
        s.onActive.background = tex;
        s.onFocused.background = tex;
        s.onHover.background = hoverTex;
        s.onHover.scaledBackgrounds = new Texture2D[]{ hoverTex };
        s.onNormal.background = tex;
        if(textColor.HasValue){
            s.normal.textColor = textColor.Value;
            s.hover.textColor = Color.yellow;
        }
        return s;
    }

    Texture2D MakeTex(Color col){
        var tex = new Texture2D(10, 10);
        var pixels = new Color[100];
        for(int i = 0; i < 100; i++) pixels[i] = col;
        tex.SetPixels(pixels); tex.Apply(); return tex;
    }

}}
