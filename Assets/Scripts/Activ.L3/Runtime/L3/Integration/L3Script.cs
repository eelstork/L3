using UnityEngine;
using System.Collections.Generic;
using Writer = Activ.XML.XmlWriter;
using Reader = Activ.XML.Xml;

namespace L3{
[CreateAssetMenu(fileName = "L3Script.asset", menuName = "L3 Script")]
public class L3Script : ScriptableObject, ISerializationCallbackReceiver{

    public Unit value = new Unit();
    public string xml;

    public void OnBeforeSerialize(){
        if(value == null)
            Debug.LogWarning("Block is empty");
        else{
            xml = Writer.Write(value);
            if(xml.Contains(":")){
                Debug.LogWarning("XML contains garbage hint");
                Debug.LogWarning("XML VIEW:\n" + xml);
            }
            //Debug.Log("XML VIEW:\n" + xml);
        }
    }

    public void OnAfterSerialize(){
        Debug.Log($"After serializing {this}");
        xml = null;
    }

    public void OnAfterDeserialize(){
        this.value = Reader.Read<Unit>(xml);
        //this.value = node as Unit;
        xml = null;
    }

}}
