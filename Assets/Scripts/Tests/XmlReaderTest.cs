using System;
using System.Collections.Generic;
using NUnit.Framework; using UnityEngine;
using Activ.XML; using Activ.XML.TestTypes;
using static Activ.XML.XmlReader;
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
        var output = Read<ChessPiece>(@chess);
        Assert.That(output, Is.InstanceOf<ChessPiece>());
        var piece = (ChessPiece) output;
        Assert.That(piece.name, Is.EqualTo("Rook"));
        Assert.That(piece.value, Is.EqualTo(5));
    }

    // NOTE - this will fail because `ChessPiece` is part of the test
    // assembly. Types partaking
    [Test] public void ReadObject_withoutTypeInfo_fails(){
        var ex = Assert.Throws<InvalidOperationException>(
            () => Read(@chess)
        );
        Assert.That(
            ex.Message,
            Is.EqualTo("No matching type for <ChessPiece>")
        );
    }

    [Test] public void ReadObject_withoutTypeInfo(){
        var output = Read(@param);
        Assert.That(output, Is.InstanceOf<L3.Parameter>());
        var _param = (L3.Parameter) output;
        Assert.That(_param.type, Is.EqualTo("int"));
        Assert.That(_param.name, Is.EqualTo("index"));
    }

    [Test] public void ReadString(){
        var output = Read<string>(@string);
        Assert.That(output, Is.InstanceOf<string>());
        Assert.That(output, Is.EqualTo("Hello"));
    }

}
