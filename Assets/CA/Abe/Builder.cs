using UnityEngine;
using v3 = UnityEngine.Vector3;
using q = UnityEngine.Quaternion;

namespace Abe{
public class Builder : MonoBehaviour{

    //public int count = 3;
    //public Transform container;
    public GameObject[] prefabs;
    public float speed = 3f;
    float? next;
    int index = 0;
    int count = 12;
    public float angular = 30f;

    void FixedUpdate(){
        self.Rotate(0f, angular * Time.deltaTime, 0f);
        if(next == null){
            next = Time.time + 1f;
        }
        if(Time.time >= next){
            Build();
            if(index > count) enabled = false;
            next = null;
        }
        rb.AddForce((self.forward * speed - rb.velocity) * rb.mass);
    }

    void Build(){
        var i = index % prefabs.Length;
        var prefab = prefabs[i];
        var instance = Instantiate(prefab).transform;
        instance.position = self.position - self.forward * 1f;
        index ++;
    }

    Rigidbody rb => GetComponent<Rigidbody>();

    //float dt => Time.deltaTime;

    Transform self => transform;

}

}
