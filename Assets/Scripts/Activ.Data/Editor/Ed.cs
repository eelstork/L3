using System.Collections; using System.Collections.Generic;
using UnityEngine; using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;
using Activ.Editor.Util; using Activ.Data.Util;
using static Activ.Data.Traversal;

namespace Activ.Data{
public class Ed{

    Object current;

    public void OnGUI(Object arg){
        current = arg;
        Traverse(new Field(arg), Visit, Children);
        current = null;
    }

    public Field[] Children(Field arg)
    => arg.value.Fields();

    public void Visit(Field arg){
        //if(arg.name == "xml") return;
        if(arg.value == null){
            if(arg.type == typeof(string)) arg.value = "";
        }
        if(arg.value == null){
            Label(arg.ToString());
        }else if(arg.value.IsAtomicallyEditable()){
            var @new = arg.value.Edit(arg.name);
            if(arg.type == typeof(string)){
                if(string.IsNullOrEmpty(@new as string)){
                    @new = null;
                }
            }
            if(@new != arg.value){
                arg.Assign(@new);
                EditorUtility.SetDirty(current);
            }
        }else{
            Label($"{arg}");
        }
    }

}}
