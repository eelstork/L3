using UnityEngine;
using v3 = UnityEngine.Vector3;
using Activ.Util;

public class Move : Task{

    public v3 dir;
    public v3? target = null;
    public float speed = 4f;
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
        if(target == null){
            target = transform.position + dir * 3f;
        }
        var P = target.Value;
        var pos = self.position;
        if(P == self.position){
            target = null; return true;
        }
        //
        bool didComplete = self.MoveTo(P, speed, out bool didFail);
        if(didFail || didComplete){
            target = null;
            return true;
        }else return false;
    }

}
