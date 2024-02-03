using UnityEngine;
using EGL = UnityEditor.EditorGUILayout;

namespace Activ.Editor.Util{
public static class ObjectExt{

    public static bool IsAtomicallyEditable(this object self)
    => self switch{
        Bounds x => true,
        BoundsInt x => true,
        Color x => true,
        AnimationCurve x => true,
        double x => true,
        float x => true,
        int x => true,
        string x => true,
        //Layer x => EGL.LayerField(x),
        long x => true,
        //MaskField x => EGL.MaskField(x),
        //Object x => EGL.ObjectField(x),
        Rect x => true,
        Vector2 x => true,
        Vector2Int x => true,
        Vector3 x => true,
        Vector4 x => true,
        _  => false
    };

    public static object Edit(this object self) => self switch{
        Bounds x => EGL.BoundsField(x),
        BoundsInt x => EGL.BoundsIntField(x),
        Color x => EGL.ColorField(x),
        AnimationCurve x => EGL.CurveField(x),
        double x => EGL.DoubleField(x),
        float x => EGL.FloatField(x),
        int x => EGL.IntField(x),
        string x => EGL.TextField(x),
        //Layer x => EGL.LayerField(x),
        long x => EGL.LongField(x),
        //MaskField x => EGL.MaskField(x),
        //Object x => EGL.ObjectField(x),
        Rect x => EGL.RectField(x),
        Vector2 x => EGL.Vector2Field("", x),
        Vector2Int x => EGL.Vector2IntField("", x),
        Vector3 x => EGL.Vector3Field("", x),
        Vector4 x => EGL.Vector4Field("", x),
        _  => null
    };

    public static object Edit(this object self, string name)
    => self switch{
        Bounds x => EGL.BoundsField(name, x),
        BoundsInt x => EGL.BoundsIntField(name, x),
        Color x => EGL.ColorField(name, x),
        AnimationCurve x => EGL.CurveField(name, x),
        double x => EGL.DoubleField(name, x),
        float x => EGL.FloatField(name, x),
        int x => EGL.IntField(name, x),
        string x => EGL.TextField(name, x),
        long x => EGL.LongField(name, x),
        //Object x => EGL.ObjectField(name, x),
        Rect x => EGL.RectField(name, x),
        Vector2 x => EGL.Vector2Field(name, x),
        Vector2Int x => EGL.Vector2IntField(name, x),
        Vector3 x => EGL.Vector3Field(name, x),
        Vector4 x => EGL.Vector4Field(name, x),
        _  => null
    };
}}
