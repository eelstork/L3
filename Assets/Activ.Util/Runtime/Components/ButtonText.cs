using UnityEngine; using UnityEngine.UI;

namespace Activ.Util{
[ExecuteInEditMode]
public class ButtonText : MonoBehaviour{

    public string text;
    public bool useObjectName=false;
    public Color color = Color.black;

    void Start(){
        if(Application.isPlaying) DestroyImmediate(this);
    }

    void Update(){
        if(useObjectName) text = gameObject.name;
        var ui = transform.GetComponentInChildren<Text>();
        if(text.None()) text = ui.text; else ui.text = text;
        ui.color = color;
    }

}}
