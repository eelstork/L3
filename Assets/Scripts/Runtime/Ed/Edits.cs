using UnityEngine;
using UnityEditor;

public static class Edits{

    public static bool didChange = true;
    public static EditorWindow graphEditor;
    public static EditorWindow nodeEditor;

    public static void Repaint(){
        //Debug.Log($"Repaint {graphEditor}, {nodeEditor}");
        graphEditor?.Repaint();
        nodeEditor?.Repaint();
    }

}
