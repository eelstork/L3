using NUnit.Framework;
using Experimental;
using UnityEngine;

namespace R1.Tests{
public class Stack0_Test{

    [Test] public void DefInStack(){
        var stack = new Stack0(); stack["x"] = 5;
        Assert.That(stack["x"], Is.EqualTo(5));
    }

    [Test] public void DefInFrame(){
        var stack = new Stack0(); stack.EnterFrame(); stack["x"] = 5;
        Assert.That(stack["x"], Is.EqualTo(5));
    }

    [Test] public void DefInBlock(){
        var stack = new Stack0(); stack.EnterBlock(); stack["x"] = 5;
        Assert.That(stack["x"], Is.EqualTo(5));
    }

    [Test] public void Hiding(){
        var stack = new Stack0();
        stack["x"] = 3;
        stack.EnterFrame();
        stack["y"] = 7;
        Assert.That(stack.Exists("x"), Is.EqualTo(false));
        Assert.That(stack["y"], Is.EqualTo(7));
    }

    [Test] public void Dump(){
        var stack = new Stack0();
        stack["a"] = 3;
        stack.EnterFrame();
        stack["b"] = 7;
        stack["c"] = 10;
        stack.EnterBlock();
        stack["d"] = 20;
        stack["e"] = 21;
        Debug.Log(stack.Dump());
    }

    [Test] public void Transparency(){
        var stack = new Stack0();
        stack["x"] = 3;
        stack.EnterBlock();
        stack["y"] = 7;
        Assert.That(stack["x"], Is.EqualTo(3));
        Assert.That(stack["y"], Is.EqualTo(7));
    }

    [Test] public void VariableOverride(){
        var stack = new Stack0();
        stack["x"] = 3;
        stack.EnterBlock();
        stack["x"] = 7;
        stack.EnterBlock();
        stack["x"] = 12;
        Assert.That(stack["x"], Is.EqualTo(12));
        stack.ExitBlock();
        Assert.That(stack["x"], Is.EqualTo(7));
    }

}}
