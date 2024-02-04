using L3;
using UnityEngine;
using static status;

public class Patrol : L3Component{

    //public Transform food => GameObject.Find("Food").transform;

    public status Logb(object arg){
        UnityEngine.Debug.Log(arg);
        return cont;
    }

}
