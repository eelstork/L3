using UnityEngine;
using GO = UnityEngine.GameObject;

namespace Activ.Util{
public static class GameObjectExt{

    public static void DestroyAfter(
        this GO self, float delay
    ) => self.AddComponent<DestroyLater>().delay = delay;

    public static bool DenotesAgent(this GO self)
    => self.GetComponentInChildren<Animator>();


}}
