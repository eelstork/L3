using System;
using System.Collections.Generic;
using NUnit.Framework; using UnityEngine;
using Activ.XML; using Activ.XML.TestTypes;
using static Activ.XML.Xml;
using static Activ.XML.XmlWriter;
using static Activ.XML.TestData;

public class XmlReaderTest{

    [Test] public void ReadArray(){
        var output = Read<int[]>(@array);
        Assert.That(output, Is.InstanceOf<int[]>());
        Assert.That(output, Is.EqualTo(new int[]{1, 2, 3}));
    }

    [Test] public void ReadEnum(){
        var output = Read<Shape>(@enum);
        Assert.That(output, Is.InstanceOf<Shape>());
        Assert.That(output, Is.EqualTo(Shape.Circle));
    }

    [Test] public void ReadInt(){
        var output = Read<int>(@int);
        Assert.That(output, Is.InstanceOf<int>());
        Assert.That(output, Is.EqualTo(5));
    }

    [Test] public void ReadList(){
        var output = Read<List<int>>(@list);
    }

    // -------------------------------------------------------------

    [Test] public void ReadObject_withTypeInfo(){
        var output = Read<ChessPiece>(@chessPiece);
        Assert.That(output, Is.InstanceOf<ChessPiece>());
        var piece = (ChessPiece) output;
        Assert.That(piece.name, Is.EqualTo("Rook"));
        Assert.That(piece.value, Is.EqualTo(5));
    }

    [Test] public void ReadBadXML(){
        var ex = Assert.Throws<System.Xml.XmlException>(
            () => Read(@badXML)
        );
    }

    [Test] public void ReadObject_with_orphan_fails(){
        var ex = Assert.Throws<InvalidOperationException>(
            () => Read(@badChessPiece)
        );
        Assert.That(
            ex.Message,
            Is.EqualTo("No [score] field in type [ChessPiece]")
        );
    }

    // NOTE - Normally when an assembly starts with 'test' the
    // type will not be found. However in our case the test assembly
    // name starts with 'activ.data' therefore we can deserialize
    // the type
    [Test] public void ReadObject_withoutTypeInfo_succeeds(){
        var result = Read(@chessPiece);
        Debug.Log(result);
    }

    [Test] public void ReadString(){
        var output = Read<string>(@string);
        Assert.That(output, Is.InstanceOf<string>());
        Assert.That(output, Is.EqualTo("Hello"));
    }

}
