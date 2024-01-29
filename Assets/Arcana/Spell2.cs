using UnityEngine;
using v3 = UnityEngine.Vector3;
using q = UnityEngine.Quaternion;

namespace Arcana{
public class Spell2 : MonoBehaviour{

    public int count = 3;
    public Transform container, prefab;
    Motion[] motions;
    public float period = 0.1f;
    float next;
    v3 p0;
    Rigidbody body;

    void OnEnable(){
        var n = count;
        motions = new Motion[n];
        for(int i = 0; i < n; i++){
            motions[i] = new Motion();
        }
        p0 = transform.position;
        next = period;
        body = GetComponent<Rigidbody>();
        //body.AddForce(Random.insideUnitSphere, ForceMode.Impulse);
    }

    void FixedUpdate(){
        var i = (int)Mathf.Floor(Time.time);
        i %= count;
        motions[i].Apply(body);
        //Trail();
    }

    void Trail(){
        if(Time.time < next) return;
        var p1 = transform.position;
        var sz = (p1 - p0).magnitude;
        if(sz > 0.5f){
            var obj = Instantiate(prefab);
            obj.SetParent(container);
            obj.localScale = v3.one * sz;
            obj.position = (p1 + p0) / 2f;
            //obj.rotation = self.rotation;
            obj.forward = p1 - p0;
            p0 = p1;
        }
        next = Time.time + period;
    }

    //float dt => Time.deltaTime;

    Transform self => transform;

    class Motion{

        public v3 acc;

        public Motion(){
            acc = Random.insideUnitSphere * 8f;
        }

        public void Apply(Rigidbody rb){
            var x = rb.transform.TransformDirection(acc);
            rb.AddForce(x * rb.mass);
            //rb.AddForce(-rb.velocity * rb.mass);
        }

        float dt => Time.deltaTime;

    }

}

}
