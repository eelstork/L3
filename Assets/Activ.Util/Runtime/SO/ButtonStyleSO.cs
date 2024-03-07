using UnityEngine;

namespace Activ.Util{
[CreateAssetMenu(
    fileName = "Button Style",
    menuName = "Activ-Utils/Button Style", order = 2
)]
public class ButtonStyleSO : ScriptableObject{

    public Color backgroundColor = Color.white;
    public Color textColor = Color.black;
    public int fontSize = -1;

}}
