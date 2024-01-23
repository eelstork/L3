using UnityEngine;
using v3 = UnityEngine.Vector3;

public class Home : Task{

    public v3 target;
    public float speed = 2f;
    Transform self;

    public Home() => target = Random.insideUnitSphere * 5f;

    public bool Exe(Transform transform){
        self = transform;
        var P = target;
        var pos = self.position;
        if(P == self.position) return true;
        //
        var vec = (P - self.position);
        var dist = vec.magnitude;
        var step = Time.deltaTime * speed;
        if(step > dist){ self.position = pos; return true; }
        var u = vec * step;
        self.position += u;
        return false;
    }

}
