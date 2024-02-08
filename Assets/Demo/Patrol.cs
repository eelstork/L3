using L3;
using UnityEngine;
using v3 = UnityEngine.Vector3;
using static status;

public class Patrol : L3Component{

    //public Transform food => GameObject.Find("Food").transform;

    public v3 right => Vector3.right;
    public v3 left => Vector3.left;
    public v3 up => Vector3.up;
    public v3 down => Vector3.down;

    public void Move(v3 arg){
        transform.position += arg * Time.deltaTime;
    }

    public status Logb(object arg){
        UnityEngine.Debug.Log(arg);
        return cont;
    }

}
