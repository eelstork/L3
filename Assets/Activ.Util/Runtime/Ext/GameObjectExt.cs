using UnityEngine;

namespace Activ.Util{
public static class GameObjectExt{

    public static void DestroyAfter(
        this GameObject self, float delay
    ) => self.AddComponent<DestroyLater>().delay = delay;

}}
