using UnityEngine;
using v3 = UnityEngine.Vector3;
using Activ.Util;

public class DropAt : Task{

    public Transform target = null;
    public float speed = 1f;
    Transform self;

    public DropAt(){}

    public bool Exe(Transform self){
        if(self.childCount == 0) return true;
        //if(target == null){
        target = Nearest(self);
        //}
        var P = target.position + v3.up * 2f;
        if(self.MoveTo(P, speed, out bool blocked)){
            if(blocked){
                //Debug.Log("blocked");
                //Debug.Break();
                target = null;
                return true;
            }
            var child = self.GetChild(0);
            child.SetParent(null);
            var rb = child.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            target = null;
            return true;
        }else{
            return false;
        }
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
