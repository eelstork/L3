using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using Activ.XML;

public class XmlReaderTest{

    [Test] public void ReadArray(){
        var output = XReader.Read<string[]>(
              "<String-Array>"
            + "<String>A</String><String>B</String>"
            + "</String-Array>"
        );
    }

    [Test] public void ReadInt(){
        var output = XReader.Read<int>( "<Int32>100</Int32>");
        Assert.That(output, Is.InstanceOf<int>());
        Assert.That(output, Is.EqualTo(100));
    }

    [Test] public void ReadList(){
        var output = XReader.Read<List<string>>(
            "<List><String>A</String><String>B</String></List>"
        );
    }

    [Test] public void ReadObject(){
        var output = XReader.Read( "<Boom/>" );
        //Assert.That(output, Is.InstanceOf<Boom>());
    }

    [Test] public void ReadString(){
        var output = XReader.Read<string>( "<String>ABC</String>" );
        Assert.That(output, Is.InstanceOf<string>());
        Assert.That(output, Is.EqualTo("ABC"));
    }

}
