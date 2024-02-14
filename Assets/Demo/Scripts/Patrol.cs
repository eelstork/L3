using L3;
using UnityEngine;
using v3 = UnityEngine.Vector3;
using static status;

public class Patrol : L3Component{

    public v3 right => Vector3.right;
    public v3 left => Vector3.left;
    public v3 up => Vector3.up;
    public v3 down => Vector3.down;
    public string targetStr;
    v3? target;

    public status Move(v3 arg){
        if(!target.HasValue){
            target = self.position + arg;
            targetStr = "Moving to " + target.ToString();
        }
        if(self.position == target.Value){
            target = null; return done;
        }else return cont;
    }

    override protected void Update(){
        base.Update();
        if(!target.HasValue || self.position == target.Value) return;
        var dir = (target.Value - self.position);
        var dist = dir.magnitude;
        dir.Normalize();
        var delta = dir * Time.deltaTime;
        if(dist > delta.magnitude){
            self.position += delta;
        }else{
            self.position = target.Value;
        }
    }

    Transform self => transform;

}
