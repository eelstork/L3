using System.Collections.Generic;

namespace L3{
public class Record{

    Frame frame;

    public void Enter(Node arg){
        var @new = new Frame(arg, parent: frame);
        if(frame != null) frame.Add(@new);
        frame = @new;
    }

    public void Exit(Node arg, object value){
        frame.value = value;
        if(frame.parent != null) frame = frame.parent;
    }

    class Frame{
        public Frame parent;
        public List<Frame> children;
        public Node node;
        public object value;
        public Frame(Node x, Frame parent) => this.node = x;
        public void Add(Frame child){
            if(children == null) children = new ();
            children.Add(child);
        }
    }

}}
