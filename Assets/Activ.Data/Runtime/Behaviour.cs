using UnityEngine;
using Activ.XML;

namespace Activ.Data{
public abstract class Behaviour: MonoBehaviour,
                                 ISerializationCallbackReceiver{

    [SerializeField] bool ignoreReadErrors;
    [SerializeField] string readError;
    [SerializeField] string xml;

    public void OnBeforeSerialize(){
        if(!hasReadError || ignoreReadErrors){
            xml = XmlWriter.Write(this);
        }else{
            Debug.LogWarning($"{name}: cannot overwrite XML; previous read had errors");
        }
    }

    public void OnAfterDeserialize(){
        try{
            if(!string.IsNullOrEmpty(xml)){
                Xml.Read(xml, this, ignoreReadErrors);
            }
            readError = null;

            //xml = null;
        }catch(System.Exception e){
            readError = e.Message;
            throw;
        }
    }

    bool hasReadError
    => !string.IsNullOrEmpty(readError);

}}
