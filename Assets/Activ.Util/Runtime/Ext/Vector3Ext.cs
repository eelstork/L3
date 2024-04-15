using UnityEngine;
using v3 = UnityEngine.Vector3;
using q4 = UnityEngine.Quaternion;
using InvOp = System.InvalidOperationException;

public static class Vector3Ext{

    public static v3[] cardinals
    => new v3[]{ v3.right, v3.forward, v3.left, v3.back };

    public static v3[] octo => new v3[]{
        v3.right, v3.up, v3.forward, v3.left, v3.down, v3.back
    };

    public static Transform Above(this v3 self, float dist=5f){
        bool didHit = Physics.Raycast(
            self, v3.up, out RaycastHit hit, dist
        );
        //ebug.DrawRay(self, v3.up * dist, Color.magenta);
        if(!didHit) return null;
        return hit.collider.transform;
    }

    public static (v3 left, v3 right) Alternative(this v3 u, v3 up){
        var left = v3.Cross(u, up).normalized;
        var right = v3.Cross(u, -up).normalized;
        return (left, right);
    }

    public static v3 Bezier(this v3 p0, v3 p1, v3 p2, float t) {
		var s = 1f - t;
		return s * s * p0 + 2f * s * t * p1 + t * t * p2;
    }

    // NOTE: Derivative; not a normal vector
    public static v3 BezierDv (this v3 p0, v3 p1, v3 p2, float t)
    => (1f - t) * (p1 - p0) + t * (p2 - p1);

    public static float CSum(this v3 self)
    => self.x + self.y + self.z;

    public static float Dist(this v3 σ, v3 τ) => v3.Distance(σ, τ);

    public static v3[] FigureOf(int n, float degs = 0f){
        var A = new v3[n]; for(var i = 0; i < n; i++){
            var a = degs * Mathf.Deg2Rad + Mathf.PI * 2f * i / n;
            A[i] = new (Mathf.Cos(a), 0f, Mathf.Sin(a));
        } return A;
     }

    public static v3 FromCSV(string arg){
        var v = arg.Split(',');
        return new v3(
            float.Parse(v[0]), float.Parse(v[1]), float.Parse(v[2])
        );
    }

    public static v3 HalfwayTo(this v3 σ, v3 τ) => (σ + τ) / 2f;

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

    public static v3 JagXZ(this v3 u){
        if(abs(u.x) > abs(u.z)) return new (u.x, 0, 0);
        if(abs(u.z) > abs(u.x)) return new (0, 0, u.z);
        return new (u.x, 0, u.z);
        float abs(float x) => x < 0 ? -x : x;
    }

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

    public static string Name(this v3 x){
        x = x.Planar();
        return (x == v3.forward) ? "north":
               (x == v3.back   ) ? "south":
               (x == v3.right  ) ? "east" :
               (x == v3.left   ) ? "west" : x.ToString();
   }

   public static v3 NextCard(this v3 x)
   => (x == v3.forward) ? v3.right:
      (x == v3.right  ) ? v3.back:
      (x == v3.back   ) ? v3.left:
      (x == v3.left   ) ? v3.forward:
      throw new InvOp($"Bad card {x}");

    public static v3 Planar(this v3 σ)
    => new v3(σ.x, 0f, σ.z);

    public static v3? Project(
        this v3 P, float maxDist, float maxAngle
    ){
        var didHit = Physics.Raycast(
            P, v3.down, out RaycastHit hit, maxDist
        );
        Debug.DrawRay(P, v3.down * maxDist, Color.cyan);
        if(!didHit) return null;
        var angle = v3.Angle(hit.normal, v3.up);
        if(angle > maxAngle) return null;
        return hit.point.Round(3);
    }

    public static void PointSameAs(this v3 self, v3 other){
        var angle = v3.Angle(self, other);
        if(angle > 90f){
            self.x = - self.x;
            self.y = - self.y;
            self.z = - self.z;
        }
        angle = v3.Angle(self, other);
        if(angle > 90f){
            Debug.LogError("point same as failed");
        }
    }

    public static v3 Round(this v3 self, int n) => new (
        (float) System.Math.Round(self.x, n),
        (float) System.Math.Round(self.y, n),
        (float) System.Math.Round(self.z, n)
    );

    public static v3? QuadXZ(this v3 u){
        if(abs(u.x) > abs(u.z)) return u.x > 0f ? v3.right  : v3.left;
        if(abs(u.z) > abs(u.x)) return u.z > 0f ? v3.forward: v3.back;
        return null;
        float abs(float x) => x < 0 ? -x : x;
    }

    public static v3 Right(this v3 σ)
    => new v3(σ.z, 0, -σ.x).normalized;

    public static v3 Raise(this v3 σ, float amount)
    => σ + v3.up * amount;

    public static v3 Rotate(this v3 σ, float angle)
    => σ.Rotate(angle, v3.up);

    public static v3 Rotate(this v3 σ, float angle, v3 axis)
    => q4.AngleAxis(angle, axis) * σ;

    public static v3 RotateDegsTowards(
        this v3 self, v3 other, float degsPerSecond
    ){
        var angle = degsPerSecond * Mathf.Deg2Rad * Time.deltaTime;
        return v3.RotateTowards(self, other, angle, 1f);
    }

    public static v3 RotateDegsTowards(
        this v3 self, v3 other, float degsPerSecond, float breakAngle
    ){
        var delta = v3.Angle(self, other);
        if(delta < breakAngle){
            var angle = degsPerSecond * Mathf.Deg2Rad * Time.deltaTime;
            return v3.RotateTowards(self, other, angle, 1f);
        }else{
            return other;
        }
    }

    public static string ToCSV2(this v3 u)
    => u.x + "," + u.y + "," + u.z;

    public static C Under<C>(this v3 self, float dist=5f){
        var x = self.Under(dist: dist);
        if(!x) return default(C);
        return x.GetComponent<C>();
    }

    public static Transform Under(this v3 self, float dist=5f){
        bool didHit = Physics.Raycast(
            self, v3.down, out RaycastHit hit, dist
        );
        //ebug.DrawRay(self, v3.down * dist, Color.magenta);
        if(!didHit){
            //Debug.Break();
            return null;
        }
        return hit.collider.transform;
    }

    public static Transform Under(
        this v3 self, float radius, float dist = 5f
    ){
        // NOTE - starting too close to an obstacle, spherecast will
        // assume we are inside the obstacle and 'let us out'...
        bool didHit = Physics.SphereCast(
            self, radius, v3.down, out RaycastHit hit, dist
        );
        if(!didHit) return null;
        return hit.collider.transform;
    }

    public static Transform Under(
        this v3 self, out v3? hit, float dist = 5f
    ){
        // NOTE - if we start too close to an obstacle,
        // spherecast will assume we are inside the obstacle
        // and 'let us out'; we do not want this to happen.
        bool didHit = Physics.Raycast(
            self, v3.down, out RaycastHit rh, dist
        );
        if(!didHit){ hit = null; return null; }
        hit = rh.point;
        return rh.collider.transform;
    }

    public static v3 WithXZ(this v3 u, v3 v) => new (v.x, u.y, v.z);
    public static v3 WithX(this v3 u, float x) => new (x, u.y, u.z);
    public static v3 WithY(this v3 u, float y) => new (u.x, y, u.z);
    public static v3 WithZ(this v3 u, float z) => new (u.x, u.y, z);

}
