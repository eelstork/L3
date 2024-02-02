using System.Collections.Generic;
using UnityEngine;
using static L3.Statuses;

namespace L3{
public class Record{

    public Frame frame;

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

    public class Frame{
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

    }

}}
