using System.Collections.Generic;
using UnityEngine;

namespace L3{
public class Record{

    public Frame frame;

    public void Enter(Node arg){
        Debug.Log($"Enter {arg}");
        Debug.Break();
        var @new = new Frame(arg, parent: frame);
        if(frame != null) frame.Add(@new);
        frame = @new;
    }

    public void Exit(Node arg, object value){
        frame.value = value;
        if(frame.parent != null) frame = frame.parent;
    }

    public class Frame{
        public Frame parent;
        public List<Frame> children;
        public Node node;
        public object value;
        public int depth;
        public Frame(Node x, Frame parent){
            this.node = x; this.parent = parent;
            depth = parent == null ? 0 : parent.depth + 1;
        }
        public void Add(Frame child){
            if(children == null) children = new ();
            children.Add(child);
        }
        override public string ToString() => node.ToString();
    }

}}
