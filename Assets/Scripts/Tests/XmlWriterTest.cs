using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Activ.XML;

public class XmlWriterTest{

    [Test] public void XmlWriter_WriteArray(){
        var output = XWriter.Write( new int[]{1, 2, 3} );
        Debug.Log(output);
    }

    [Test] public void XmlWriter_WriteDictionary(){
        var dic = new Dictionary<string, int>{
            {"First", 1}, {"Second", 2},
        };
        var output = XWriter.Write( dic );
        Debug.Log(output);
    }

    [Test] public void XmlWriter_WriteInt(){
        var output = XWriter.Write(5);
        Debug.Log(output);
    }

    [Test] public void XmlWriter_WriteList(){
        var output = XWriter.Write( new List<int>(){1, 2, 3} );
        Debug.Log(output);
    }

    [Test] public void XmlWriterWriteObject(){
        var output = XWriter.Write(new Simple());
        Debug.Log(output);
    }

    [Test] public void XmlWriter_WriteString(){
        var output = XWriter.Write("Hello");
        Debug.Log(output);
    }

    class Simple{
        public int value = 5;
        public string label = "five";
    }

}
