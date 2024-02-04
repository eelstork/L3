using NUnit.Framework;
using Experimental;
using UnityEngine;

namespace R1.Tests{
public class StackTest{

    [Test] public void DefInStack(){
        var stack = new Stack(); stack["x"] = 5;
        Assert.That(stack["x"], Is.EqualTo(5));
    }

    [Test] public void DefInFrame(){
        var stack = new Stack(); stack.EnterFrame(); stack["x"] = 5;
        Assert.That(stack["x"], Is.EqualTo(5));
    }

    [Test] public void DefInBlock(){
        var stack = new Stack(); stack.EnterBlock(); stack["x"] = 5;
        Assert.That(stack["x"], Is.EqualTo(5));
    }

    [Test] public void FindInTargetObject(){
        var stack = new Stack( new SimpleMap() );
        stack.EnterFrame(target : new object() );
        var result = stack["ToString"];
        Assert.That(result, Is.Not.Null);
        var str = result.ToString();
        Assert.That(str, Is.EqualTo("System.String ToString()"));
        Debug.Log(stack.Dump());
    }

    [Test] public void ObjectTransparency(){
        var stack = new Stack( new SimpleMap() );
        stack.EnterFrame(target : new object() );
        stack.EnterBlock(); // ENTER A BLOCK
        var result = stack["ToString"];
        Assert.That(result, Is.Not.Null);
        var str = result.ToString();
        Assert.That(str, Is.EqualTo("System.String ToString()"));
        Debug.Log(stack.Dump());
    }

    class SimpleMap : Map{
        public object Find(object target, object key, out bool found){
            if(target == null){
                found = false;
                return null;
            }
            var type = target.GetType();
            var m = type.GetMethod(key.ToString());
            found = (m != null);
            return m;
        }
    }

    [Test] public void Hiding(){
        var stack = new Stack();
        stack["x"] = 3;
        stack.EnterFrame();
        stack["y"] = 7;
        Assert.That(stack.Exists("x"), Is.EqualTo(false));
        Assert.That(stack["y"], Is.EqualTo(7));
    }

    [Test] public void Dump(){
        var stack = new Stack();
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
        var stack = new Stack();
        stack["x"] = 3;
        stack.EnterBlock();
        stack["y"] = 7;
        Assert.That(stack["x"], Is.EqualTo(3));
        Assert.That(stack["y"], Is.EqualTo(7));
    }

    [Test] public void VariableOverride(){
        var stack = new Stack();
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
