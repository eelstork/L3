#if UNITY_EDITOR
using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;

public partial class Function{

    override public void OnGUI(){
        base.OnGUI();
        //
        Parameter del = null;
        if(parameters != null){
            Label("Parameters");
            foreach(var p in parameters){
                BeginHorizontal();
                p.type = EGL.TextField(p.type);
                p.name = EGL.TextField(p.name);
                if(Button("x", Width(32))) del = p;
                EndHorizontal();
            }
            parameters.Remove(del);
        }
        //
        Space(8);
        BeginHorizontal();
        Act("Add Parameter", AddParameter);
        Act("Use Composite", UseComposite);
        Act("Use Call", UseCall);
        Button("Delete");
        EndHorizontal();
        Space(8);
    }

}

#endif
