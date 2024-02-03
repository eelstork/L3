using System;
using System.Collections.Generic;
using UnityEngine;
using static L3.Statuses;
using Node = L3.Node;

namespace R1{
public class Record : TNode{

    public Frame frame;
    public readonly DateTime date;
    public readonly float time;

    public TNode[] children => new TNode[]{ frame };

    public Record(){
        date = DateTime.Now;
        time = Time.time;
    }

    public void Enter(Node arg){
        var @new = new Frame(arg, parent: frame);
        if(frame != null) frame.Add(@new);
        frame = @new;
    }

    public void Exit(Node arg, object value){
        frame.value = value;
        if(frame.parent != null) frame = frame.parent;
    }

    public void ExitWithError(Node arg, System.Exception err){
        frame.error = err;
        if(frame.parent != null) frame = frame.parent;
    }

    public class Frame : TNode{
        public Frame parent;
        public List<Frame> children;
        public Node node;
        public System.Exception error;
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
        override public string ToString()
        => ValueChar() + (' ' + node.ToString());

        public char ValueChar(){
            if(error != null) return 'E';
            if(value == null) return '-';
            if(IsCont(value)) return '→';
            if(IsDoneStatus(value)) return '✓';
            if(IsFailing(value)) return '✗';
            return '+';
        }

        TNode[] TNode.children => children.ToArray();

    }

}}
