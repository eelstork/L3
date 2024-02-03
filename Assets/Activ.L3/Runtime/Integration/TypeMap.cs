using System.Collections;
using System.Collections.Generic;
using Type = System.Type;
using UnityEngine;

namespace L3{
public static class TypeMap{

    public static Dictionary<string, Type> types = new(){
        {"List", typeof(ArrayList)},
        {"Vector2", typeof(Vector2)},
        {"Vector3", typeof(Vector3)},
    };

}}
