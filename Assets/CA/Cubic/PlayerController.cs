using v2 = UnityEngine.Vector2;
using UnityEngine;

//[ExecuteInEditMode]
public class PlayerController : MonoBehaviour{

    public string colliderName;
    public float x, y;
    public float targetSpeed = 3f;
    public float currentSpeed;

    void FixedUpdate(){
        x = Input.GetAxis("Horizontal");
        //y = Input.GetAxis("Vertical");  // that will be for jumping
        var y = 0f;
        currentSpeed = rb.velocity.magnitude;
        var u = rb.velocity;           // current velocity
        u.y = 0f;  // don't cancel gravity
        var v = new v2(x, y) * targetSpeed;  // target velocity
        var F = (v - u) * rb.mass / Time.fixedDeltaTime;
        rb.AddForce(F, ForceMode2D.Force);
    }

    Rigidbody2D rb => GetComponent<Rigidbody2D>();

    Transform self => transform;

}
