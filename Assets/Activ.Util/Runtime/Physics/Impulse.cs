using UnityEngine;
using v3 = UnityEngine.Vector3;

namespace Activ.Util{
public partial class Impulse : MonoBehaviour{

    public v3 direction = v3.forward;
    public float amount = 10f;
    public bool apply;

    void Update(){
        if(!apply) return;
        transform.Rb().AddForce(
            direction.normalized * amount,
            ForceMode.Impulse
        );
        apply = false;
    }

}}
