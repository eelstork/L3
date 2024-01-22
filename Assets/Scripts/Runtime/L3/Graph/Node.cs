using EGL = UnityEditor.EditorGUILayout;
using UnityEngine;

[System.Serializable] public abstract partial class Node{

    public string name;

    public Node() => name = GetType().Name.ToUpper();

    public virtual string TFormat() => name;

}
