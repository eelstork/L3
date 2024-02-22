using UnityEngine; using v3 = UnityEngine.Vector3;
using Activ.Util;

namespace Activ.Util.Ph{
public class Torque : MonoBehaviour{

    public v3 axis = v3.forward;
    public float magnitude = 1f;

    void FixedUpdate(){
        var X = self.TransformDirection(axis);
        Debug.DrawRay(pos, X, Color.yellow);
        self.Rb().AddTorque(axis.normalized * magnitude);
    }

    v3 pos => self.position;
    Transform self => transform;

}}
