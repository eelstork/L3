using UnityEngine;
using System.Collections.Generic;

namespace L3{
[CreateAssetMenu(
    fileName = "Unit.asset",
    menuName = "L3/Unit")
] public class Unit : ScriptableObject, ISerializationCallbackReceiver{

    public Function func = new Function();
    public string xml;

    public void OnBeforeSerialize(){
        var writer = new XMLWriter();
        xml = writer.ToXML(func);
    }

    public void OnAfterDeserialize(){
        var reader = new XMLReader();
        var node = reader.FromXML(xml);
        this.func = node as Function;
        xml = null;
    }

}}
