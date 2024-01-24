using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Activ.XML;
using static Activ.XML.XmlWriter;
using Activ.XML.TestTypes;

public class XmlWriterTest{

    [Test] public void WriteArray(){
        var output = Write( new int[]{1, 2, 3} );
        Log(output);
    }

    [Test] public void WriteDictionary_BAD(){
        var dic = new Dictionary<string, int>{
            {"First", 1}, {"Second", 2}
        };
        var output = Write( dic );
        Log(output);
    }

    [Test] public void WriteEnum(){
        var output = Write( Shape.Circle );
        Log(output);
    }

    [Test] public void WriteInt(){
        var output = Write(5);
        Log(output);
    }

    [Test] public void WriteList(){
        var output = Write( new List<int>(){1, 2, 3} );
        Log(output);
    }

    [Test] public void WriteObject(){
        var output = Write(new ChessPiece("Rook", 5));
        Log(output);
    }

    [Test] public void WriteString(){
        var output = Write("Hello");
        Log(output);
    }

    void Log(string arg) => UnityEngine.Debug.Log(
        arg.Replace('"', '\'')
    );

}
