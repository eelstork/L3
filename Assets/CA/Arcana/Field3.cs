using UnityEngine;
using v3 = UnityEngine.Vector3;

public class Field3 : MonoBehaviour{

    public float k = 5f;
    public int count = 10;
    public v3 sz = v3.one * 0.3f;
    public GameObject prefab;

    void OnEnable(){
        for(float x = -k; x < k; x += k / count)
        for(float y = -k; y < k; y += k / count)
        for(float z = -k; z < k; z += k / count){
            var w = Instantiate(prefab).transform;
            w.localScale = sz;
            w.position = new v3(x, y, z);
        }
    }

}
