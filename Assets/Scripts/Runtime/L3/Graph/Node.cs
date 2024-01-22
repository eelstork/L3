using EGL = UnityEditor.EditorGUILayout;
using UnityEngine;

namespace L3{
public abstract partial class Node{

    public string name;

    public Node() => name = GetType().Name.ToUpper();

    public virtual string TFormat() => name;

}}
