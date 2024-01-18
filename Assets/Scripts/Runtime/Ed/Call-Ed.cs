#if UNITY_EDITOR
using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;

public partial class Call{

    override public void OnGUI(){
        base.OnGUI();
        //
        Space(8);
        BeginHorizontal();
        Act("Add Number", AddNumber);
        Act("Add String", AddString);
        Act("Add Variable", AddVar);
        Button("Delete");
        EndHorizontal();
        Space(8);
    }

}

#endif
