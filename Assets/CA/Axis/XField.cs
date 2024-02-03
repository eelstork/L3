using UnityEngine;

public class XField : MonoBehaviour{

    public int count = 25;
    public float radius = 10;

    void Start(){
        for(int i = 0; i < count; i++){
            var clone = Instantiate(this.gameObject);
            var f = clone.GetComponent<XField>();
            Destroy(f);
            var P = self.position + Random.insideUnitSphere
                           * radius;
            P.y = self.position.y;
            clone.transform.position = P;
        }
    }

    Transform self => transform;

}
