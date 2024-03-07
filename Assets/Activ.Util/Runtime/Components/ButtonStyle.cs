using UnityEngine; using UnityEngine.UI;

namespace Activ.Util{
[ExecuteInEditMode]
public class ButtonStyle : MonoBehaviour{

    public Color backgroundColor = Color.white;
    public Color textColor = Color.black;
    public int fontSize = -1;
    [Header("Config overrides settings")]
    public ButtonStyleSO config;

    void Update(){
        if(Application.isPlaying){ Destroy(this); return; }
        foreach(var k in GetComponentsInChildren<Button>())
        StyleButton(k);
    }

    void StyleButton(Button button){
        button.GetComponentInChildren<Image>().color = _bg;
        var text = button.GetComponentInChildren<Text>();
        text.color = _textColor;
        if(_fontSize > - 1) text.fontSize = _fontSize;
    }

    Color _bg => config?.backgroundColor ?? backgroundColor;
    int _fontSize => config?.fontSize ?? fontSize;
    Color _textColor => config?.textColor ?? textColor;

}}
