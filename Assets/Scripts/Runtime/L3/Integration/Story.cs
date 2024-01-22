using System.Collections.Generic;
/*
public class Story{

    Frame frame;

    public void Enter(Node arg){
        if(frame.children[i] == arg){
            frame = frame.children[i];
        }else{
            frame.Archive();
            var @new = new Frame(arg, parent: frame);
            frame.Add(@new);
            frame = @new;
        }
    }

    public void Exit(Node arg, object value){
        frame.value = value;
        // TODO even if the frame does not have more nodes,
        // if it has been created during this tick, it should remain
        // 'open' so that it can be extended
        if(frame.hasMore){
            frame.Archive();
        }else{
            frame.index = 0;
        }
        if(frame.parent != null){
            frame.parent.index++;
            frame = frame.parent;
        }
    }

    class Frame{

        public List<Episode> archive = new ();

        public Frame parent;
        public List<Frame> trail;
        public int index;
        public Node node;
        public object value;

        public Frame(Node x, Frame parent) => this.node = x;

        public void Add(Frame child){
            if(children == null) children = new ();
            children.Add(child);
        }

        public void Archive(){
            // Do the following:
            // (1) duplicate the trail, up to 'index', exclusive.
            // (2) archive the trail
            // (3) the duplicate is assigned as current
        }

        public class Episode{}

    }

}
*/
