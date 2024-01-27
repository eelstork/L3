using UnityEngine;
using v3 = UnityEngine.Vector3;
using T = UnityEngine.Transform;

public static class TransformExt{

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
            var u = vec.normalized * step;
            self.position += u;
            return false;
        }
    }

    public static bool HasLOS(this T self, v3 dir, float dist){
        var didHit = Physics.Raycast(
            self.position, dir, out RaycastHit hit, dist
        );
        return !didHit;
    }

}
