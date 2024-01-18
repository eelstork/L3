#if UNITY_EDITOR
using EGL = UnityEditor.EditorGUILayout;

public partial class Var{

    override public void OnGUI(){
        base.OnGUI();
        value = EGL.TextField("Value", value);
    }

}

#endif
