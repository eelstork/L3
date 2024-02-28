using UnityEngine;
using UnityEngine.UI;
//using Activ.Util;
using v3 = UnityEngine.Vector3;

namespace Activ.Util{
public class ImageColorFade : MonoBehaviour{

    public Color target;
    public float speed = 0.01f;
    public Image image;

    void Start(){
        if(!image) image = GetComponent<Image>();
    }

    public void Lerp(Color target, float speed=-1f){
        if(speed != -1f) this.speed = speed;
        this.target = target;
    }

    void Update(){
        image.color = Color.Lerp(image.color, target, speed);
    }

}}
