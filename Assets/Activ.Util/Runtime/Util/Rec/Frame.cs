using System.Collections.Generic;

namespace Activ.Util.Rec{
public class Frame<T>{

    public Frame<T> parent;
    public List<Frame<T>> children;
    public T node;
    public System.Exception error;
    public object value;
    public int depth;

    public Frame(T x, Frame<T> parent){
        this.node = x; this.parent = parent;
        depth = parent == null ? 0 : parent.depth + 1;
    }

    public void Add(Frame<T> child)
    => (children ?? (children = new ())).Add(child);

    override public string ToString(){
        var str = node.ToString();
        return value == null ? str
             : $"[{value}] {str}";
    }

    int childCount => children?.Count ?? 0;

}}
