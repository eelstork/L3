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
    public float responseTime = 0.1f;

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
        var dt = Time.fixedDeltaTime;
        rb.AddForce(F0 / (responseTime * (Mathf.Sqrt(1f + currentSpeed)) ), ForceMode.Force);
    }

    Rigidbody rb => GetComponent<Rigidbody>();

    Transform self => transform;

}
