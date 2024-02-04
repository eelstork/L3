using System.Collections.Generic;
using InvOp = System.InvalidOperationException;
using System.Linq; using Activ.Util;

namespace Experimental{
public class Stack0{

    Stack<Frame> s = new ();

    public Stack0(){ EnterFrame(); }

    public void EnterFrame(){ s.Push(new Frame()); EnterBlock();}
    public void ExitFrame () => s.Pop();
    public void EnterBlock() => f.Push( new Block() );
    public void ExitBlock () => f.Pop();

    public bool Exists(object key){
        foreach(var x in f) if(x.ContainsKey(key)) return true;
        return false;
    }

    public object this[object key]{
        get{
            foreach(var x in f) if(x.ContainsKey(key)) return x[key];
            throw new InvOp($"Not found: {key}");
        }
        set => b[key] = value;
    }

    Frame f => s.Peek(); Block b => f.Peek();

    class Frame : Stack<Block>{}

    class Block : Dictionary<object, object>{}

    // --------------------------------------------------------------

    public string Dump(){
        var z = "stack\n"; foreach(var f in s.Reverse()) Dmpf(f, 1);
        void Dmpf(Frame f, int d){
            z += "frame\n".Tabs(d);
            foreach(var b in f.Reverse()) Dmpb(b, d + 1);
        }
        void Dmpb(Block b, int d){
            z += "block\n".Tabs(d); foreach(var kv in b)
                z += $"{kv.Key}: {kv.Value}\n".Tabs(d + 1);
        } return z;
    }

}}
