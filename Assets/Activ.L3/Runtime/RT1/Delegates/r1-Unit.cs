using System; using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Activ.Util; using L3;

namespace R1{
public static class Unit{

    public static object Step(
        L3.Unit unit, Context cx, HashSet<Node> deps
    ){
        //cx.Log("u/" + unit);
        // NOTE we do not enter the namespace. As a result
        // imported objects are available
        //cx.stack.Push(new Scope());
        ImportUnits(unit, cx, deps);
        Pose(unit, cx);
        object value = null;
        if(unit.nodes != null){
            foreach(var k in unit.nodes) value = cx.Step(k);
        }
        return value;
    }

    static void Pose(L3.Unit unit, Context cx){
        if(unit.@as.None()) return;
        var pose = cx.pose.GetType().Name;
        if(unit.@as == pose){
            //Debug.Log($"Already posing as {pose}");
            return;
        }
        var type = Activ.Util.Types.Find(unit.@as);
        cx.pose = Activator.CreateInstance(type);
        Debug.Log($"Posing as '{unit.@as}': [{cx.pose}] via [{type}]");
    }

    static void Import(
        string unit, L3Script[] @from, Context cx, HashSet<Node> deps
    ){
        if(string.IsNullOrEmpty(unit)) return;
        Debug.Log($"Import [{unit}]");
        foreach(var k in @from){
            if(k.value.ns == unit){
                if(deps.Contains(k.value)){
                    Debug.Log($"Already included {k.value}");
                }else{
                    deps.Add(k.value);
                    Debug.Log($"Include dep {k} {k.value}");
                    cx.Step(k.value);
                }
            }
        }
    }

    static void ImportUnits(
        L3.Unit unit, Context cx, HashSet<Node> deps
    ){
        var modules = Activ.Util.Assets.FindAll<L3Script>();
        if(unit.deps == null) return;
        foreach(var x in unit.deps.Split(",")){
            Import(x, modules, cx, deps);
        }
    }


}}
