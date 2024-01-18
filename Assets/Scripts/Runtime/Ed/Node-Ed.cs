#if UNITY_EDITOR
using static UnityEngine.GUILayout;
using UnityEngine;
using EGL = UnityEditor.EditorGUILayout;

public abstract partial class Node{

    public virtual void OnGUI(){
        name = EGL.TextField(GetType().Name, name);
    }

    public void Act(string str, System.Action act){
        if(!Button(str)) return;
        act();
        Edits.didChange = true;
        Edits.Repaint();
    }

    public void Act(System.Action act)
    => Act(act.Method.Name, act);

}

#endif
