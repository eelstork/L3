using System;
using System.Collections.Generic;
//using System.Linq;
using NUnit.Framework;
//using static UnityEngine.Debug;
//using static Activ.Data.Traversal;
using Activ.Util;

//namespace Activ.Util.Tests{
public class ListExtTest{

    List<int> list;

    [SetUp] public void Setup() => list = new List<int>(){0, 1, 4};

    [Test] public void Dequeue(){
        var x = list.Dequeue();
        Assert.That(list.Count, Is.EqualTo(2));
        Assert.That(x, Is.EqualTo(4));
    }

    [Test] public void Enqueue(){
        list.Enqueue(3);
        Assert.That(list[0], Is.EqualTo(3));
    }

    [Test] public void EnqueueFirst(){
        list = new ();
        list.Enqueue(3);
        Assert.That(list[0], Is.EqualTo(3));
    }

    [Test] public void EnqueueAscending1(){
        list.Enqueue(3, IntComparison);
        Assert.That(list[2], Is.EqualTo(3));
    }

    [Test] public void EnqueueAscending2(){
        list.Enqueue(5, IntComparison);
        Assert.That(list[3], Is.EqualTo(5));
    }

    [Test] public void EnqueueFirstWithComparer(){
        list.Enqueue(3, IntComparison);
        Assert.That(list[2], Is.EqualTo(3));
    }

    int IntComparison(int x, int y) => x.CompareTo(y);

}
//}
