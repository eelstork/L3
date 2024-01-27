using System; using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine; using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;
using Field = System.Reflection.FieldInfo;

public class ObjectInspector{

    bool didUseAction;
    bool didEdit;

    public void OnGUI(
        object arg, out bool didUseAction, out bool didEdit
    ){
        this.didUseAction = false;
        this.didEdit = false;
        var type = arg.GetType(); Label(type.Name.ToUpper()); Space(8);
        OnGUI(arg, type);
        EditorActions(arg);
        didUseAction = this.didUseAction;
        didEdit = this.didEdit;
    }

    void OnListGUI(string name, IList list){
        Label(name); int i = 0; foreach(var x in list){
            OnListElementGUI(x, ++i);
        }
    }

    void OnListElementGUI(object arg, int index){
        var type = arg.GetType();
        BeginHorizontal();
        Label(index + ": ");
        OnGUI(arg, type, hz: true);
        EndHorizontal();
    }

    void OnGUI(object arg, Type type, bool hz=false){
        if(type == null) return;
        OnGUI(arg, type.BaseType, hz);
        foreach(var field in type.GetFields()){
            if(IsHidden(field)) continue;
            if(field.DeclaringType == type) OnGUI(arg, field, hz);
        }
    }

    void EditorActions(object arg){
        var type = arg.GetType();
        if(type == null) return;
        BeginHorizontal();
        foreach(var method in type.GetMethods()){
            if(IsEditorAction(method) && Button(method.Name)){
                method.Invoke(arg, null);
                didUseAction = true;
            }
        }
        EndHorizontal();
    }

    void OnGUI(object arg, Field field, bool hz){
        var value = field.GetValue(arg); var name = field.Name;
        if(field.FieldType.IsEnum){
            EditEnum(arg, field, value as Enum, hz);
        }else switch(value){
            case bool @bool:
                EditBool(arg, field, @bool, hz); break;
            case float @float:
                EditFloat(arg, field, @float, hz); break;
            case string str:
                EditString(arg, field, str, hz); break;
            case IList list:
                OnListGUI(name, list); break;
            default:
                Label(field.Name + ": " + ToString(value));
                break;

        }
    }

    bool IsEditorAction(MethodInfo arg)
    => Attribute.IsDefined(arg, typeof(EditorActionAttribute));

    bool IsHidden(Field arg)
    => Attribute.IsDefined(arg, typeof(HideInInspector))
    || Attribute.IsDefined(arg, typeof(HierarchyAttribute));

    void EditBool(object owner, Field field, bool value, bool hz){
        var @new = Toggle(field.Name, value, hz);
        if(@new == value) return;
        field.SetValue(owner, @new);
        didEdit = true;
    }

    void EditEnum(object owner, Field field, Enum value, bool hz){
        var @new = EnumPopup(field.Name, value, hz);
        if(@new == value) return;
        field.SetValue(owner, @new);
        didEdit = true;
    }

    void EditString(object owner, Field field, string value, bool hz){
        var @new = TextField(field.Name, value, hz);
        if(@new == value) return;
        field.SetValue(owner, @new);
        didEdit = true;
    }

    void EditFloat(object owner, Field field, float value, bool hz){
        var @new = FloatField(field.Name, value, hz);
        if(@new == value) return;
        field.SetValue(owner, @new);
        didEdit = true;
    }

    bool Toggle(string label, bool value, bool hz){
        if(hz) return EGL.Toggle(value);
        else return EGL.Toggle(label, value);
    }

    Enum EnumPopup(string label, Enum value, bool hz){
        if(hz) return EGL.EnumPopup(value);
        else return EGL.EnumPopup(label, value);
    }

    float FloatField(string label, float value, bool hz){
        if(hz) return EGL.FloatField(value);
        else return EGL.FloatField(label, value);
    }

    string TextField(string label, string value, bool hz){
        if(hz) return EGL.TextField(value);
        else return EGL.TextField(label, value);
    }

    public string ToString(object arg)
    => arg == null ? "null" : arg.ToString();

}
