//using EGL = UnityEditor.EditorGUILayout;
using UnityEngine;

namespace L3{
public abstract partial class Node{

    private Branch _parent;

    public Node(){}

    public virtual string TFormat(bool ex) => ToString();

    public void SetParent(Branch x) => _parent = x;

    public Branch parent => _parent;

}}
