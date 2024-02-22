using InvOp = System.InvalidOperationException;
using UnityEngine;
using v3 = UnityEngine.Vector3;
using T = UnityEngine.Transform;

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

    public static Transform View(this T self){
        var view = self.GetChild(0);
        if(view.name != "View") throw new InvOp($"Not view: {view}");
        return view;
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

    public static Vector3 Point(this T self, float x, float y, float z)
    => self.TransformPoint(new (x, y, z));

    public static float Radius(this T self, bool planar){
        var collider = self.GetComponent<Collider>();
        return collider.bounds.Radius(planar);
    }

    public static void SetKinematic(this T self, bool flag=true)
    => self.Rb().isKinematic = flag;

    public static Rigidbody Rb(this T self)
    => self.GetComponent<Rigidbody>();

    public static void UnlockAll(this T self)
    => self.Rb().constraints = RigidbodyConstraints.None;

    public static void UnlockRot(this T self)
    => self.Rb().constraints = RigidbodyConstraints.FreezePosition;

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

    public static bool HasLOS(this T self, v3 dir, float dist){
        var didHit = UnityEngine.Physics.Raycast(
            self.position, dir, out RaycastHit hit, dist
        );
        return !didHit;
    }

}}
