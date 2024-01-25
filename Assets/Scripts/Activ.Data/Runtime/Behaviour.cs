using UnityEngine;
using Activ.XML;

namespace Activ.Data{
public abstract class Behaviour: MonoBehaviour,
                                 ISerializationCallbackReceiver{

    public string xml;

    public void OnBeforeSerialize(){
        xml = XmlWriter.Write(this);
        Debug.Log("XML VIEW:\n" + xml);
    }

    public void OnAfterSerialize(){
        Debug.Log($"After serializing {this}");
        xml = null;
    }

    public void OnAfterDeserialize(){
        XmlReader.Read(xml, this);
        xml = null;
    }

}}
