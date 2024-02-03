using UnityEngine;
using v3 = UnityEngine.Vector3;
using q = UnityEngine.Quaternion;

namespace Arcana{
public class Spell : MonoBehaviour{

    public int count = 3;
    public Transform container, prefab;
    Motion[] motions;
    public float period = 0.1f;
    float next;
    v3 p0;

    void OnEnable(){
        var n = count;
        motions = new Motion[n];
        for(int i = 0; i < n; i++){
            motions[i] = new Motion();
        }
        p0 = transform.position;
        next = period;
    }

    void Update(){
        var i = (int)Mathf.Floor(Time.time);
        i %= count;
        motions[i].Apply(self);
        if(Time.time >= next){
            var p1 = transform.position;
            var obj = Instantiate(prefab);
            obj.localScale = v3.one * (p1 - p0).magnitude;
            obj.position = (p1 + p0) / 2f;
            obj.rotation=self.rotation;
            obj.SetParent(container);
            p0 = p1;
            next = Time.time + period;
        }
    }

    //float dt => Time.deltaTime;

    Transform self => transform;

    class Motion{

        public float speed, acc;
        v3 spin;
        float alpha;

        public Motion(){
            speed = Random.value;
            acc = Random.value - 0.5f;
            spin = Random.insideUnitSphere.normalized;
            alpha = Random.value * 1800f - 900f;
        }

        public void Apply(Transform arg){
            arg.position += arg.forward * speed * dt;
            speed += acc * dt;
            var q0 = q.AngleAxis(alpha * dt, spin);
            arg.localRotation = q0 * arg.localRotation;
        }

        float dt => Time.deltaTime;

    }

}

}
