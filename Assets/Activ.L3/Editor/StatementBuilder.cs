using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine; using UnityEditor;
using static UnityEngine.GUILayout;
using EGL = UnityEditor.EditorGUILayout;
using GL = UnityEngine.GUILayout;
using InvOp = System.InvalidOperationException;
using Activ.Util;
using L3;
using static L3.Composite.Type;

// TODO this is just more traversal, correct? Is every code traversal?
// At some level it HAS to be. So, are declarative, type capturing
// traversals genuinely less code? Less fuss? Is that really a thing?
// Long winded
public partial class StatementBuilder{

    L3Script script;
    //
    MethodInfo method; MemberInfo prop; Branch target;
    int[] @params;
    string[] paramIds;
    StatementBuilder sbuilder = null;

    public void OnGUI(L3Script script, Branch target){
        this.target = target; this.script = script;
        if(method != null) BuildCall();
        else if(prop != null) BuildAccess();
        else ChooseMember();
        if(canBuild && Button("OK", Width(60))) DoBuildCall();
    }

    public void OnGUI(MemberInfo member, L3Script script){
        this.script = script;
        if(method != null){
            BuildCall();
        }else if(prop != null){
            BuildAccess();
        }else{
            var curr = member switch{
                PropertyInfo pi => pi.PropertyType,
                FieldInfo fi => fi.FieldType,
                _ => throw new InvOp($"Only fields and props: {member}")
            };
            ChooseMember(curr);
        }
    }

    void ChooseMember(Type type){
        Space(8);
        Label($"C'd from {type.Name} ...");
        Space(8);
        ChooseMethod(type);
        ChooseFieldOrProp(type);
    }

    void ChooseMember(){
        if(target == null) return;
        var pose = script.unit.@as; if(pose.None()) return;
        var type = Activ.Util.Types.Find(pose);
        if(type == null) return;
        Space(8);
        Label($"From {type.Name} ...");
        Space(8);
        ChooseMethod(type);
        ChooseFieldOrProp(type);
    }

    void ChooseMethod(Type type){
        var methods = type.DecMethods(); var i = 0;
        BeginHorizontal(); foreach(var k in methods){
            var str = k.Name;
            if(Button(str, minw)){
                method = k;
                var n = k.GetParameters().Length;
                @params = new int[n]; @paramIds = new string[n];
            }
            if(i++ == 7){ EndHorizontal(); i = 0; BeginHorizontal(); }
        } EndHorizontal();
        Space(8);
    }

    void ChooseFieldOrProp(Type type){
        var props = type.DecFieldsOrProps(); var i = 0;
        BeginHorizontal(); foreach(var k in props){
            var str = k.Name;
            if(Button(str, GL.ExpandWidth(false))){
                prop = k; @params = null; @paramIds = null;
                sbuilder = new StatementBuilder();
            }
            if(i++ == 7){ EndHorizontal(); i = 0; BeginHorizontal(); }
        } EndHorizontal();
        Space(8);
    }

    public bool canBuild{ get{
        if(method != null){
            foreach(var k in paramIds){
                if(k.None()) return false;
            }
            return true;
        }else if(prop != null){
            return sbuilder.canBuild;
        }else return false;
    }}

    public void DoBuildCall(){
        var node = CreateCall();
        (target as Branch).AddChild(node);
        method = null; prop = null; sbuilder = null;
    }

    public Node CreateCall(){
        if(method != null){
            var call = new Call(method.Name);
            foreach(var str in @paramIds){
                call.AddChild( new Var(str) );
            }
            return call;
            //(target as Branch).AddChild(call);
            //method = null;
        }else{
            return new Composite(Composite.Type.access, new Node[]{
                new Var(prop.Name),
                sbuilder.CreateCall()
            });
        }

    }

    GUILayoutOption minw => GL.ExpandWidth(false);

}
