using UnityEngine;
using T = UnityEngine.Transform;

namespace Activ.Util{
public static class ComponentExt{

    public static bool Has<T>(this Component self) where T : Component
    => self.GetComponent<T>();

    public static bool Within(this Component self, float dist, T of)
    => self.Dist(of) < dist;

    public static float Dist(this Component self, T arg)
    => (self.transform.position - arg.position).magnitude;

}}
