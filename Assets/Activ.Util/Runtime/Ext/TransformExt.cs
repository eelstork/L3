using InvOp = System.InvalidOperationException;
using UnityEngine;
using v3 = UnityEngine.Vector3;
using T = UnityEngine.Transform;
using static UnityEngine.Mathf;

namespace Activ.Util{
public static class TransformExt{

    public static float AheadStep(this T arg, float maxH = 0.2f){
        var P = arg.position
            + arg.TransformDirection(new (0f, maxH * 2, 1.2f));
        var didHit = Physics.Raycast(
            P, v3.down, out RaycastHit hit, maxH * 3
        );
        if(!didHit) return 0f;
        var delta = hit.point.y - arg.position.y;
        return delta > maxH ? 0f : delta;
    }

    public static void Clear(this T self){
        if(Application.isPlaying){
            foreach(T k in self) Object.Destroy(k.gameObject);
        }else{
            for(var i = self.childCount - 1; i >= 0 ; i--){
                Object.DestroyImmediate(self.GetChild(i).gameObject);
            }
        }
    }

    public static v3 Dir(this T self, T arg, bool planar){
        var u = arg.position - self.position;
        if(planar) u.y = 0f;
        return u.normalized;
    }

    public static bool HasLOS(this T self, T arg){
        var P = self.position;
        var u = arg.position - P;
        var dist = u.magnitude;
        var didHit = UnityEngine.Physics.Raycast(P, u, out RaycastHit hit, dist);
        if(!didHit) return true;
        // TODO sometimes an object has several colliders
        if(hit.collider == arg.GetComponent<Collider>()){
            return true;
        }else{
            return false;
        }
    }

    public static bool HasLOS(
        this T self, v3 dir, out T collider, float dist
    ){
        var didHit = UnityEngine.Physics.Raycast(
            self.position, dir, out RaycastHit hit, dist
        );
        if(didHit){ collider = hit.collider.transform; return false; }
        else{ collider = null; return true; }
    }

    public static bool HasLOS(
        this T self, v3 dir, float dist, bool debugDraw=false
    ){
        v3 P = self.position; RaycastHit hit;
        var didHit = Physics.Raycast(P, dir, out hit, dist);
        if(debugDraw) Debug.DrawRay(
            P, dir.normalized * dist,
            didHit ? Color.red : Color.green
        ); return !didHit;
    }

    public static bool IsCoaxial(
        this T self, T arg, float threshold=5
    ){
        var u = arg.Dir(self, planar: true);
        var A = v3.Angle(u, arg.forward);
        var B = v3.Angle(u, arg.right);
        var C = v3.Angle(u, - arg.forward);
        var D = v3.Angle(u, - arg.right);
        if(A < threshold) return true;
        if(B < threshold) return true;
        if(C < threshold) return true;
        if(D < threshold) return true;
        return false;
    }

    public static bool IsOn(this T self, T arg){
        var didHit = UnityEngine.Physics.Raycast(
            self.position, v3.down, out RaycastHit hit, 2f
        );
        if(didHit)
            return hit.collider == arg.GetComponent<Collider>();
        else
            return false;
    }

    public static bool IsTouching(this T self, T arg){
        //var b0 = self.GetComponent<Collider>().bounds;
        //var b1 = arg.GetComponent<Collider>().bounds;
        //if(b0.Intersects(b1)) return true;
        if(self.Has<Rigidbody>())
            return self.IsTouchingAssumingSelfRB(arg);
        else if(arg.Has<Rigidbody>())
            return arg.IsTouchingAssumingSelfRB(self);
        throw new InvOp($"Neither {self} nor {arg} have rigidbod");
    }

    static bool IsTouchingAssumingSelfRB(this T self, T arg){
        var t = self.GetComponent<CollisionTracker>();
        if(!t) t = self.gameObject.AddComponent<CollisionTracker>();
        return t.IsContacting(arg, @unsafe: true);
    }

    public static bool IsUnder(
        this T self, Collision arg, float maxAngle=45f
    ){
        var n = arg.contactCount;
        if(n == 0) return false;
        var P = self.position;
        for(var i = 0; i < n; i++){
            var u = arg.GetContact(i).point - P;
            var angle = v3.Angle(v3.down, u);
            if(angle > maxAngle) return false;
        }
        return true;
    }

    public static bool IsNonPhysical(this T self){
        var rb = self.GetComponent<Rigidbody>();
        if(!rb) return true;
        if(rb.isKinematic) return true;
        return false;
    }

    public static bool IsPhysical(this T self)
    => self.GetComponent<Rigidbody>();

    public static bool IsPhysical(this T self, out Rigidbody rb)
    => rb = self.GetComponent<Rigidbody>();

    public static T JoinedObject(this T self){
        var joint = self.GetComponent<FixedJoint>();
        if(joint == null) return null;
        return joint.connectedBody.transform;
    }

    public static void LockRot(this T self)
    => self.Rb().constraints = RigidbodyConstraints.FreezeRotation;

    public static void LockPos(this T self)
    => self.Rb().constraints = RigidbodyConstraints.FreezePosition;

    public static void LockPosXZ(this T self)
    => self.Rb().constraints
        = RigidbodyConstraints.FreezePositionX
        | RigidbodyConstraints.FreezePositionZ;

    public static void LockY(this T self)
    => self.Rb().constraints = RigidbodyConstraints.FreezePositionY;

    public static bool MoveTo(
        this T self, v3 P, float speed, out bool didFail
    ){
        didFail = false;
        var vec = P - self.position;
        var dist = vec.magnitude;
        var step = Time.deltaTime * speed;
        if(step > dist){
            self.position = P;
            return true;
        }else{
            if(!self.HasLOS(vec, 0.7f)){
                didFail = true;
                return true;
            }
            var vec1 = new v3(vec.x, 0f, vec.z);
            if(!self.HasLOS(vec1, 0.7f)){
                if(vec.y > 0f) vec = v3.up;
                else vec = v3.down;
            }
            var u = vec.normalized * step;
            self.position += u;
            return false;
        }
    }

    public static v3 NearestCoaxialPosition(
        this T self, T arg, float radius
    ){
        var u = arg.transform.right * radius;
        var v = arg.transform.forward * radius;
        var P0 = self.position;
        var P1 = arg.position;
        var D = float.MaxValue;
        v3 Q = v3.zero;
        for(var i = -1; i <= 1; i += 2){
            var P = P1 + u * i;
            var w = (P - P0); w.y = 0;
            var d = w.magnitude;
            if(d < D){ Q = P; D = d; }
        }
        for(var j = -1; j <= 1; j+=2){
            var P = P1 + v * j;
            var w = (P - P0); w.y = 0;
            var d = w.magnitude;
            if(d < D){ Q = P; D = d; }
        }
        return Q;
    }

    public static void PMoveAndOrient(  // call in FixedUpdate
        this Transform self, Transform target, v3 offset,
        float latency = 0.06f, float speed = 10,
        float decayDist = 0.2f, float scalar=1f
    ){
        PMove(
            self, target, offset, latency, speed, decayDist,
            relativePos: false, scalar: scalar
        );
        POrient2(self, target);
    }

    public static float PMove(  // call in FixedUpdate
        this Transform self, Transform target, v3 offset,
        float latency=0.06f, float speed=10,
        float decayDist=0.2f, bool relativePos=true, float scalar=1f
    ){
        // Move it
        v3 P = relativePos ? target.TransformPoint(offset)
                           : target.position + offset;
        var u = (P - self.position);
        var d = u.magnitude;
        if(d < decayDist){
            u = u.normalized * speed * d / decayDist;
        }else{
            u = u.normalized * speed;
        }
        Debug.DrawLine(self.position, P, Color.yellow);
        var rb = self.Rb();
        var v = u - rb.velocity;
        rb.AddForce(v * scalar * rb.mass / latency);
        return d;
    }

    // Axis-align, minimizing effort
    public static float PAlign(  // call in FixedUpdate
        this Transform self, Transform target, float latency = 0.2f
    ){
        var rb = self.Rb();
        var u0 = target.ClosestAxis(self.up);
        var r0 = target.ClosestAxis(self.right);
        var u = v3.Cross(self.up, u0);
        var v = v3.Cross(self.right, r0);
        var w = rb.angularVelocity;
        u -= w; v -= w;
        rb.AddTorque((u + v) / 2 * (rb.mass / latency));
        var a0 = v3.Angle(self.up, u0);
        var a1 = v3.Angle(self.right, r0);
        return (a0 + a1) / 2f;
    }

    public static v3 ClosestAxis(this Transform s, v3 u){
        var v = s.right; var a = v3.Angle(u, s.right);
        C(-s.right); C(s.up); C(-s.up); C(s.forward); C(-s.forward);
        return v; void C(v3 w){
            var b = v3.Angle(u, w); if(b < a){ v = w; a = b; }
        }
    }

    public static void MakeYAxisPointUp(this Transform self)
    => AxisUp.Y(self);

    /*
    public static void MakeYAxisPointUp(this Transform self){
        var X = v3.Angle(self.right, v3.up);
        var Y = v3.Angle(self.up, v3.up);
        var Z = v3.Angle(self.forward, v3.up);
        if(Y < X && Y < Z && Y < 80) return;
        //ebug.Log("Rotate around Z axis (90)");
        self.Rotate(0, 0, 90);
        Y = v3.Angle(self.up, v3.up);
        //ebug.Log($"Y is now at {Y:0} degs from Up");
        if(Y > 80){
            //ebug.Log("Rotate around Z axis (180)");
            self.Rotate(0, 0, 180);
        }
        //ebug.Log("Rotate around X axis (90)");
        self.Rotate(90, 0, 0);
        Y = v3.Angle(self.up, v3.up);
        //ebug.Log($"Y is now at {Y:0} degs from Up");
        if(Y > 80){
            //ebug.Log("Rotate around X axis (180)");
            self.Rotate(180, 0, 0);
        }
    }*/

    public static float POrient2(  // call in FixedUpdate
        this Transform self, Transform target, float latency = 0.2f
    ){
        var rb = self.Rb();
        var u = v3.Cross(self.up, target.up) / 2;
        var v = v3.Cross(self.right, target.right) / 2;
        var w = rb.angularVelocity;
        u -= w;
        v -= w;
        rb.AddTorque((u + v) * rb.mass / latency);
        var a0 = v3.Angle(self.up, target.up);
        var a1 = v3.Angle(self.right, target.right);
        return (a0 + a1) / 2f;
    }

    // Works like a basic spring; does not cancel existing angular
    // velocity
    public static float POrient(  // call in FixedUpdate
        this Transform self, Transform target, float latency = 0.2f
    ){
        var rb = self.Rb();
        var u = v3.Cross(self.up, target.up) / 2;
        var v = v3.Cross(self.right, target.right) / 2;
        rb.AddTorque((u + v) * rb.mass / latency);
        var a0 = v3.Angle(self.up, target.up);
        var a1 = v3.Angle(self.right, target.right);
        return (a0 + a1) / 2f;
    }

    public static Vector3 Point(
        this T self, float x, float y, float z
    ) => self.TransformPoint(new (x, y, z));

    public static float Radius(this T self, bool planar){
        var collider = self.GetComponent<Collider>();
        return collider.bounds.Radius(planar);
    }

    public static C Reachable<C>(
        this T self, float angle=0f,
        float h=0.75f, float r=0.5f, float dist=1.6f
    ){
        var a = angle * Deg2Rad;
        var u = new v3(0, -Sin(a), Cos(a));
        return self.Reachable<C>(dir: u, h, r, dist);
    }

    public static C Reachable<C>(
        this T self, v3 dir,
        float h=0.75f, float r=0.5f, float dist=2.0f
    ){
        var P = self.position + v3.up * h + self.forward * r;
        var u = self.TransformDirection(dir).normalized;
        var didHit = Physics.Raycast(P, u, out RaycastHit hit, dist);
        var c = didHit ? hit.collider.GetComponent<C>() : default(C);
        if(didHit) Debug.DrawLine(
            P, hit.point, c != null ? Color.green : Color.yellow
        ); else Debug.DrawRay(P, u * dist, Color.red);
        return c;
    }

    public static float Redress(  // call in FixedUpdate
        this Transform self, float speed, float latency = 0.2f
    ){
        var rb = self.Rb();
        var u = v3.Cross(self.up, v3.up) * speed
              - rb.angularVelocity;
        rb.AddTorque(u * rb.mass / latency);
        var a = v3.Angle(self.up, v3.up);
        return a;
    }

    public static Rigidbody Rb(this T self)
    => self.GetComponent<Rigidbody>();

    public static void SetKinematic(this T self, bool flag=true)
    => self.Rb().isKinematic = flag;

    public static T Under(this T self, float dist=5f)
    => self.position.Under(dist);

    public static T UnderWithRadius(this T self, float radius)
    => self.position.Under(radius);

    public static void UnlockAll(this T self)
    => self.Rb().constraints = RigidbodyConstraints.None;

    public static void UnlockRot(this T self)
    => self.Rb().constraints = RigidbodyConstraints.FreezePosition;

    public static Transform View(this T self){
        var view = self.GetChild(0);
        if(view.name != "View") throw new InvOp($"Not view: {view}");
        return view;
    }

}}
