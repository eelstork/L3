using UnityEngine;

public class ExitAction : MonoBehaviour{

    public float delay = 1f;

    public void OnEnable() => Invoke("Quit", delay);

    public void Quit(){
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

}
