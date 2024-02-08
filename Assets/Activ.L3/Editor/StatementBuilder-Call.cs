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
public partial class StatementBuilder{  // Build call

    void BuildCall(){
        if(method == null) return;
        BeginHorizontal();
        Label(method.Name + " (", minw);
        var parameters = method.GetParameters();
        var n = parameters.Length;
        for(var i = 0; i < n; i++){
            var k = parameters[i];
            Label(k.Name + ": " + k.ParameterType.Name, minw);
            ChooseArg(k.ParameterType, i);
            if(i < n - 1) Label(", ", minw);
        }
        Label(") ", minw);
        Space(16);
        if(Button("X", minw)){ method = null; }
        EndHorizontal();
    }

    void ChooseArg(Type arg, int i){
        Type type;
        // NOTE - even when recursing, arguments still come from
        // the root type retrieved from the context script
        var pose = script.unit.@as; if(pose.None()) return;
        type = Activ.Util.Types.Find(pose);
        var fields = type.DecFieldsOrProps(arg);  //var i = 0;
        if(fields.Length == 0){
            //ebug.Log($"No fields/props of type [{arg}] in type [{type}]");
            if(@paramIds[i] == null) @paramIds[i] = "";
            @paramIds[i] = GL.TextField(@paramIds[i], 60);
        }else{
            var choices = (from x in fields select x.Name).ToArray();
            @params[i] = EGL.Popup(@params[i], choices, GL.Width(60));
            @paramIds[i] = choices[@params[i]];
        }
    }

}
