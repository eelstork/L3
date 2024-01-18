#if UNITY_EDITOR
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;

public partial class Composite{

    override public void OnGUI(){
        base.OnGUI();
        //
        Space(8);
        BeginHorizontal();
        Act("Add Call", AddCall);
        Act(Delete);
        EndHorizontal();
        Space(8);
    }

}

#endif
