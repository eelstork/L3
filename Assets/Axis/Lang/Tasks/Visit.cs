using UnityEngine;
using v3 = UnityEngine.Vector3;

public class Visit : Task{

    public v3? target = null;
    public float speed = 1f;
    Transform self;

    public Visit(){
        var u = Random.insideUnitSphere;
    }

    public bool Exe(Transform transform){
        self = transform;
        if(target == null){
            target = Nearest(self).position;
        }
        var P = target.Value;
        var pos = self.position;
        if(P == self.position){
            target = null; return true;
        }
        //
        var vec = (P - self.position);
        var dist = vec.magnitude;
        var step = Time.deltaTime * speed;
        if(step > dist){
            self.position = pos; target = null; return true;
        }
        var u = vec.normalized * step;
        self.position += u;
        return false;
    }

    public Transform Nearest(Transform self){
        var candidates = Object.FindObjectsOfType<Rigidbody>();
        Transform @sel = null;
        var D = float.MaxValue;
        foreach(var x in candidates){
            var d = v3.Distance(x.transform.position, self.position);
            if(d < D){
                @sel = x.transform;
                D = d;
            }
        }
        return @sel;
    }

}
