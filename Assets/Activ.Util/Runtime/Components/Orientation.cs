using UnityEngine;
using v3 = UnityEngine.Vector3;

namespace Activ.Util{
[DisallowMultipleComponent]
public class Orientation : MonoBehaviour{

    public float angle, delta, angularVelocity;

    public static implicit operator float(Orientation self)
    => self.angle;

    void Start() => angle = EvalAngle();

    void Update(){
        var u = transform.forward.Planar();
        var angle = EvalAngle();
        delta = angle - this.angle;
        angularVelocity = delta / Time.deltaTime;
        this.angle = angle;
    }

    float EvalAngle()
    => v3.SignedAngle(self.forward.Planar(), v3.forward, v3.up);

    Transform self => transform;

}}
