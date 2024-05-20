using UnityEngine;
using T = UnityEngine.Transform;

namespace Activ.Util{
public static class ComponentExt{

    public static T Add<T>(this Component self) where T: Component
    => self.gameObject.AddComponent<T>();

    public static T Get<T>(this Component self)
    => self.GetComponent<T>();

    public static string Name(this Component self)
    => self.gameObject.name;

    public static T Own<T>(this Component self, bool up=false){
        T c; bool OK() => c != null;
        c = self.GetComponent<T>();             if(OK()) return c;
        c = self.GetComponentInChildren<T>();   if(OK()) return c;
        if(up){
            c = self.GetComponentInParent<T>(); if(OK()) return c;
        }
        Debug.LogError(
            $"Expected own {typeof(T)} (up: {up}) in {self}", self
        ); return default(T);
    }

    public static T Owning<T>(this Component self, bool up=false){
        var c = self.GetComponentInParent<T>();
        if(c != null ) return c; else Debug.LogError(
            $"Expected owning {typeof(T)} from {self}", self
        ); return default(T);
    }

    public static bool Has<T>(this Component self) where T : Component
    => self.GetComponent<T>();

    public static T Req<T>(this Component self) where T: Component{
        var c = self.GetComponent<T>();
        if(c != null) return c;
        return self.gameObject.AddComponent<T>();
    }

    public static bool Within(this Component self, float dist, T of)
    => self.Dist(of) < dist;

    public static float PlanarDist(this Component self, T arg)
    => (self.transform.position - arg.position).Planar().magnitude;

    public static float Dist(this Component self, T arg)
    => (self.transform.position - arg.position).magnitude;

    public static float Dist(this Component self, Vector3 arg)
    => (self.transform.position - arg).magnitude;

}}
