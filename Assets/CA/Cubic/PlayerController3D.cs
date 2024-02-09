using v3 = UnityEngine.Vector3;
using UnityEngine;

public class PlayerController3D : MonoBehaviour{

    public string colliderName;
    public float x, y;
    public float targetSpeed = 3f;
    public float currentSpeed;
    public float force;
    public float peak;
    public v3 F0;
    //[Range(0.1f, 2f)]
    //public float response = 1f;
    //[Range(0.0001f, 1f)]
    //public float tightness = 0.1f;
    //public float clamp = 1f;

    void FixedUpdate(){
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");
        //var y = 0f;
        currentSpeed = rb.velocity.magnitude;
        var u = rb.velocity * 1f;           // current velocity
        //u.y = 0f;  // don't cancel gravity
        var v = new v3(x, y) * targetSpeed;  // target velocity
        var F = (v - u) * rb.mass;
        F0 = v3.Lerp(F0, F, 1f);
        //if(F0.magnitude > clamp){
        //    F0 = F0.normalized * clamp;
        //}
        force = F0.magnitude;
        if(force > peak) peak = force;
        rb.AddForce(F0, ForceMode.Impulse);
    }

    Rigidbody rb => GetComponent<Rigidbody>();

    Transform self => transform;

}
