using UnityEngine;
using v3 = UnityEngine.Vector3;

public class Grab : Task{

    public Transform target = null;
    public float speed = 1f;
    Transform self;

    public Grab(){
    }

    public bool Exe(Transform self){
        if(self.childCount > 0) return true;
        if(target == null){
            target = Nearest(self);
        }
        var P = target.position;
        var pos = self.position;
        if(P == self.position){
            return Collect(self);
        }
        //
        var vec = (P - self.position);
        var dist = vec.magnitude;
        var step = Time.deltaTime * speed;
        if(step > dist){
            self.position = pos;
            return Collect(self);
        }
        var u = vec.normalized * step;
        self.position += u;
        return false;
    }

    bool Collect(Transform self){
        Debug.Log("Collecting", self);
        //Debug.Break();
        target.parent = self;
        var rb = target.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        target = null; return true;
    }

    public Transform Nearest(Transform self){
        var candidates = Object.FindObjectsOfType<Rigidbody>();
        Transform @sel = null;
        var D = float.MaxValue;
        foreach(var x in candidates){
            if(x.transform.parent != null) continue;
            var d = v3.Distance(x.transform.position, self.position);
            if(d < D){
                @sel = x.transform;
                D = d;
            }
        }
        return @sel;
    }

}
