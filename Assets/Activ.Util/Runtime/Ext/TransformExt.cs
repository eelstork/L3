using InvOp = System.InvalidOperationException;
using UnityEngine; using v3 = UnityEngine.Vector3;
using T = UnityEngine.Transform;

namespace Activ.Util{
public static class TransformExt{

    public static T JoinedObject(this T self){
        var joint = self.GetComponent<FixedJoint>();
        if(joint == null) return null;
        return joint.connectedBody.transform;
    }

    public static bool IsTouching(this T self, T arg){
        var b0 = self.GetComponent<Collider>().bounds;
        var b1 = arg.GetComponent<Collider>().bounds;
        if(b0.Intersects(b1)) return true;
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
        var didHit = Physics.Raycast(P, u, out RaycastHit hit, dist);
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

    public static void SetKinematic(this T self, bool flag=true){
        var rb = self.GetComponent<Rigidbody>();
        rb.isKinematic = flag;
    }

    public static bool HasLOS(this T self, v3 dir, float dist){
        var didHit = Physics.Raycast(
            self.position, dir, out RaycastHit hit, dist
        );
        return !didHit;
    }

}}
