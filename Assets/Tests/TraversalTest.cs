using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static UnityEngine.Debug;
using static Activ.Data.Traversal;

public class TraversalTest{

    [Test] public void Traverse_1(){
        string str = null;
        Traverse(new Node("A"), x => x.children, x => str += x.name);
        Log(str);
    }

    [Test] public void Traverse_3(){
        string str = null;
        var root = new Node("A");
        root.Add(new Node("B"));
        root.Add(new Node("C"));
        Traverse(root, x => x.children, x => str += x.name);
        Log(str);
    }

    [Test] public void Traverse_4(){
        string str = null;
        var A = new Node("A");
        var B = A.Add(new Node("B"));
        var C = B.Add(new Node("C"));
        var D = A.Add(new Node("D"));
        Traverse(A, x => x.children, x => str += x.name);
        Log(str);
    }

    class Node{
        public string name;
        public List<Node> children = new ();
        public Node(string x) => name = x;
        public Node Add(Node child){
            children.Add(child); return child;
        }
    }

}
