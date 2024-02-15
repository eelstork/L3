using UnityEngine;
using v3 = UnityEngine.Vector3;
using q4 = UnityEngine.Quaternion;

public static class Vector3Ext{

    public static float Dist(this v3 σ, v3 τ) => v3.Distance(σ, τ);

    public static v3 HalfwayTo(this v3 σ, v3 τ) => (σ + τ) / 2f;

    public static v3 Left(this v3 σ)
    => new v3(-σ.z, 0, σ.x).normalized;

    public static int Major(this v3 σ)
    => σ.x > σ.y && σ.x > σ.z ? 0
     : σ.y > σ.z              ? 1
     :                          2;

    // Move this point towards 'other', covering at least 'min' and
    // leaving 'margin' before the destination; if 'other' is too
    // close, and it is not possible to cover the minimum distance
    // while leaving the desired margin, return null.
    public static v3? MoveTowards(
        this v3 self, v3 other, float min, float margin)
    {
        var u = other - self;
        var d = u.magnitude - margin;
        return d >= min ? self + u.normalized * d : null;
    }

    // Assuming 'self' represents an agent of radius 'radius',
    // move either directly to or (if the target has a collider) near
    // the target position.
    public static v3? MoveTo(
        this v3 self, Transform target, float radius=0f
    ){
        var u = target.position - self;
        bool didHit = Physics.SphereCast(
            self, radius, u, out RaycastHit hit, u.magnitude
        );
        if(!didHit) return target.position;
        var collider = target.GetComponentInChildren<Collider>();
        if(hit.collider == collider){
            var v = (hit.point - self);
            return self + v.normalized * (v.magnitude - radius);
        }else{
            return null;
        }
    }

    public static v3 MoveUp(this v3 σ, float h) => σ + v3.up * h;

    public static v3 Planar(this v3 σ)
    => new v3(σ.x, 0f, σ.z);

    public static v3 Right(this v3 σ)
    => new v3(σ.z, 0, -σ.x).normalized;

    public static v3 Raise(this v3 σ, float amount)
    => σ + v3.up * amount;

    public static v3 Rotate(this v3 σ, float angle)
    => σ.Rotate(angle, v3.up);

    public static v3 Rotate(this v3 σ, float angle, v3 axis)
    => q4.AngleAxis(angle, axis) * σ;

    public static bool HasClearLOS(
        this v3 self, v3 target
    ){
        var u = target - self;
        bool didHit = Physics.Raycast(
            self, u, out RaycastHit hit, u.magnitude
        ); return !didHit;
    }

    public static bool HasClearPath(
        this v3 self, v3 target, float radius
    ){
        // NOTE - if we start too close to an obstacle,
        // spherecast will assume we are inside the obstacle
        // and 'let us out'; we do not want this to happen.
        if(!self.HasClearLOS(target)) return false;
        var u = target - self;
        bool didHit = Physics.SphereCast(
            self, radius, u, out RaycastHit hit, u.magnitude
        ); return !didHit;
    }

}
