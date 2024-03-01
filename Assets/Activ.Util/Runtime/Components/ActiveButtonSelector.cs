using UnityEngine;
using UnityEngine.UI;
using Activ.Util;

public class ActiveButtonSelector : MonoBehaviour{

    public Button active;

    void Start(){
        foreach(var k in buttons) k.onClick.AddListener(
            () => Select(k)
        );
    }

    public void Select(Button x){
        foreach(var k in buttons) k.Own<Text>().color = Color.black;
        x.Own<Text>().color = Color.white;
        active = x;
    }

    public static implicit operator Button(ActiveButtonSelector self)
    => self.active;

    Button[] buttons => GetComponentsInChildren<Button>();

}
