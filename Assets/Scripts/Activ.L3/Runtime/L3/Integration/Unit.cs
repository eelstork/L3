using UnityEngine;
using System.Collections.Generic;
using Writer = Activ.XML.XmlWriter;
using Reader = Activ.XML.XmlReader;

namespace L3{
[CreateAssetMenu(fileName = "Unit.asset", menuName = "L3/Unit")]
public class Unit : ScriptableObject, ISerializationCallbackReceiver{

    public Composite value = new Composite();
    public string xml;

    public void OnBeforeSerialize(){
        if(value == null)
            Debug.LogWarning("Block is empty");
        else{
            xml = Writer.Write(value);
            Debug.Log("XML VIEW:\n" + xml);
        }
    }

    public void OnAfterSerialize(){
        Debug.Log($"After serializing {this}");
        xml = null;
    }

    public void OnAfterDeserialize(){
        var node = Reader.Read(xml);
        this.value = node as Composite;
        xml = null;
    }

}}
