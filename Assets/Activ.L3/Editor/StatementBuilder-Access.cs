using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine; using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;
using GL = UnityEngine.GUILayout;
using Activ.Util;
using L3;
using static L3.Composite.Type;

// TODO this is just more traversal, correct? Is every code traversal?
// At some level it HAS to be. So, are declarative, type capturing
// traversals genuinely less code? Less fuss? Is that really a thing?
// Long winded
public partial class StatementBuilder{  // Access field or prop

    void BuildAccess(){
        if(prop == null) return;
        BeginHorizontal();
        Label(prop.Name);
        sbuilder.OnGUI(prop, script);
        //var parameters = method.GetParameters();
        //var n = parameters.Length;
        //for(var i = 0; i < n; i++){
        //    var k = parameters[i];
        //    Label(k.Name + ": " + k.ParameterType.Name);
        //    ChooseObject(k.ParameterType, i);
        //}
        //Space(16);
        //if(Button("Build")) DoBuildAccess();
        if(Button("Cancel")){ prop = null; }
        EndHorizontal();
    }

    public void DoBuildAccess(){
        //var call = new Call(method.Name);
        //foreach(var str in @paramIds){
        //    call.AddChild( new Var(str) );
        //}
        //(target as Branch).AddChild(call);
        prop = null;
    }

}
