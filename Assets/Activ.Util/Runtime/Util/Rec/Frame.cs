//using System;
using System.Collections.Generic;
using Node = System.Object;

namespace Activ.Util.Rec{
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

    public void Add(Frame child)
    => (children ?? (children = new ())).Add(child);

    int childCount => children?.Count ?? 0;
}}
