using UnityEngine; using UnityEngine.UI;

namespace Activ.Util{
[ExecuteInEditMode]
public class FontSize : MonoBehaviour{

    public int fontSize = -1;

    void Start(){
        if(Application.isPlaying) DestroyImmediate(this);
    }

    void Update(){
        if(fontSize <= 0) return;
        foreach(var k in GetComponentsInChildren<Text>()){
            k.fontSize = fontSize;
        }
    }

}}
