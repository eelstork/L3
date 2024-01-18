#if UNITY_EDITOR
using EGL = UnityEditor.EditorGUILayout;

public partial class Number{

    override public void OnGUI(){
        base.OnGUI();
        value = EGL.FloatField("Value", value);
    }

}

#endif
