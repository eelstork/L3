using UnityEngine;
using Activ.XML;

namespace Activ.Data{
public abstract class Model : ScriptableObject,
                              ISerializationCallbackReceiver{

    [SerializeField, HideInInspector] string xml;

    public void OnBeforeSerialize(){
        xml = XmlWriter.Write(this);
    }

    public void OnAfterDeserialize(){
        XmlReader.Read(xml, this);
        xml = null;
    }

}}
