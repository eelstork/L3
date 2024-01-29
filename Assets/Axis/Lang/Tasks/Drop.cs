using UnityEngine;
using v3 = UnityEngine.Vector3;

public class Drop : Task{

    public Transform target = null;
    public float speed = 1f;
    Transform self;

    public Drop(){}

    public bool Exe(Transform self){
        if(self.childCount == 0) return true;
        //Debug.Log("Drop", self);
        //Debug.Break();
        var child = self.GetChild(0);
        if(child.transform.position.y < 0.5){
            return true;
        }
        child.SetParent(null);
        var rb = child.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        return true;
    }

}
