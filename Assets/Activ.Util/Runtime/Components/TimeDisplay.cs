using UnityEngine;
using UnityEngine.UI;

namespace Activ.Util{
[ExecuteInEditMode]
public partial class TimeDisplay : MonoBehaviour{

    void Update(){
        text.text = ((int)(Time.time)).ToString() + "s";
    }

    public Text text => GetComponent<Text>();

}}
