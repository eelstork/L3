using v2 = UnityEngine.Vector2;
using v2i = UnityEngine.Vector2Int;
using v3 = UnityEngine.Vector3;
using static UnityEngine.Mathf;

public static class Vector2Ext{

    public static v2i Intra(this v2 a) => new (
        (int) (a.x > 0 ? Floor(a.x) : - Floor(-a.x)),
        (int) (a.y > 0 ? Floor(a.y) : - Floor(-a.y))
    );

    public static v3 Div(this v2 a, v2 b)
    => new (a.x / b.x, a.y / b.y);

    public static v2 RotateTowards(
        this v2 self, v2 other, float maxRadiansDelta
    ){
        var angle = v2.Angle(self, other);
        var maxDegreesDelta = maxRadiansDelta * UnityEngine.Mathf.Rad2Deg;
        var step = UnityEngine.Mathf.Min(maxDegreesDelta, angle);
        return UnityEngine.Quaternion.Euler(0f, 0f, step) * self;
    }

    public static v3 ToV3(this v2 σ)
    => new v3(σ.x, 0f, σ.y);

    public static v3 XYtoXZ(this v2 σ)
    => new v3(σ.x, 0f, σ.y);

    public static v3 WithX(this v2 σ, float x)
    => new v3(x, σ.y, σ.x);

    public static v3 WithY(this v2 σ, float y)
    => new v3(σ.x, y, σ.y);

    public static v3 WithZ(this v2 σ, float z)
    => new v3(σ.x, σ.y, z);

}
