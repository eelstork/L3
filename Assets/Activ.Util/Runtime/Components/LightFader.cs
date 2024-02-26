using UnityEngine;

namespace Activ.Util{
public class LightFader : MonoBehaviour{

    Color color;
    public float lerp = 0.01f;
    public Color startColor = Color.black;

    void Start(){
        color = _light.color;
        _light.color = startColor;
    }

    void Update(){
        _light.color = Color.Lerp(
            _light.color, color, lerp
        );
    }

    Light _light => GetComponent<Light>();

}}
