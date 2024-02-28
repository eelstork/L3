using System.Collections.Generic;
using Doc = System.Xml.XmlDocument;
using T = System.Xml.XmlElement; using K = System.String;
using V = System.String; using N = System.String;
using A = System.Xml.XmlAttribute;
using System.Linq;

namespace Activ.LTP{
public class XMLReader : Reader<T, A, K, V, N>{

    Doc doc;

    public XMLReader(string data){
        doc = new Doc(); doc.LoadXml(data);
    }

    public T root => doc.DocumentElement;

    public N Id(T elem) => elem.Name;

    public (K key, V value) Map(A arg) => (arg.Name, arg.Value);

    public A[] Props(T elem){
        var x = elem.Attributes; var y = new A[x.Count];
        x.CopyTo(y, 0); return y;
    }

    public T[] Children(T elem){
        var x = elem.ChildNodes; var y = new List<T>();
        foreach(var e in x) if(e is T) y.Add((T)e);
        return y.ToArray();
    }

}}
