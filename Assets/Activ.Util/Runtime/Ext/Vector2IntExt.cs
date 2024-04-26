using v2i = UnityEngine.Vector2Int;
using InvOp = System.InvalidOperationException;

public static class Vector2IntExt{

    public static bool IsCard(this v2i u)
    => u == v2i.up || u == v2i.down || u == v2i.right || u == v2i.left;

    public static Cardinal Card(this v2i u){
        if(u == v2i.up) return Cardinal.North;
        if(u == v2i.down) return Cardinal.South;
        if(u == v2i.right) return Cardinal.East;
        if(u == v2i.left) return Cardinal.West;
        throw new InvOp($"Not a cardinal vector: {u}");
    }

}
