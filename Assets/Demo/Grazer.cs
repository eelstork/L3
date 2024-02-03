using L3;
using UnityEngine;
using static status;

public class Grazer : L3Component{

    public Transform food => GameObject.Find("Food").transform;

    public status Reach(Transform arg){
        if(Dist(arg) < 0.5f) return done;
        transform.position += Dir(arg) * Time.deltaTime;
        return cont;
    }

    public Vector3 Dir(Transform arg)
    => (arg.position - self.position).normalized;

    public float Dist(Transform arg)
    => (arg.position - self.position).magnitude;

    public Transform self => transform;

}
