using EGL = UnityEditor.EditorGUILayout;
using UnityEngine;

namespace L3{
public abstract partial class Node{

    public Node(){}

    public virtual string TFormat() => ToString();

}}
