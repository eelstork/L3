using Doc = System.Xml.XmlDocument;
//
using T = System.Xml.XmlElement;
// using A = System.Object;
using K = System.String;
using V = System.String;
using N = System.String;

namespace Activ.LTP{
public class XMLWriter : Writer<T, K, V, N>{

    Doc doc;

    public string data => doc.OuterXml;

    public XMLWriter() => doc = new Doc();

    public T Materialize(N id, T under){
        var e = doc.CreateElement(id);
        if(under == null){
            doc.AppendChild(e);            
        }else{
            under.AppendChild(e);
        }
        return e;
    }

    public void Assign(K key, V value, T e)
    => e.SetAttribute(key, value);

}}
