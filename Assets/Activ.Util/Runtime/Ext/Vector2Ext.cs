using v2 = UnityEngine.Vector2;
using v3 = UnityEngine.Vector3;

public static class Vector2Ext{

    public static v3 XYtoXZ(this v2 σ)
    => new v3(σ.x, 0f, σ.y);

    public static v3 WithX(this v2 σ, float x)
    => new v3(x, σ.y, σ.x);

    public static v3 WithY(this v2 σ, float y)
    => new v3(σ.x, y, σ.y);

    public static v3 WithZ(this v2 σ, float z)
    => new v3(σ.x, σ.y, z);

}
