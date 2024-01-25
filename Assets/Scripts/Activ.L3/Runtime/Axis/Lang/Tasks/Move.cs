using UnityEngine;
using v3 = UnityEngine.Vector3;

public class Move : Task{

    public v3 dir;
    public v3? target = null;
    public float speed = 1f;
    Transform self;

    public Move(){
        var u = Random.insideUnitSphere;
        dir = new v3(
            Mathf.Round(u.x),
            Mathf.Round(u.y),
            Mathf.Round(u.z)
        );
    }

    public bool Exe(Transform transform){
        self = transform;
        if(target == null) target = transform.position + dir;
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
        var u = dir.normalized * step;
        self.position += u;
        return false;
    }

}
