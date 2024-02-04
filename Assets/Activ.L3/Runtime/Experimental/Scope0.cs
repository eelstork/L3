using System.Collections.Generic;
using InvOp = System.InvalidOperationException;
using System.Linq;

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

    public string Dump(){
        var z = "stack\n";
        foreach(var f in s.Reverse()) f.Dump(ref z, 1);
        return z;
    }

    public object this[object key]{
        get{
            foreach(var x in f) if(x.ContainsKey(key)) return x[key];
            throw new InvOp($"Not found: {key}");
        }
        set => b[key] = value;
    }

    Frame f => s.Peek(); Block b => f.Peek();

    class Frame : Stack<Block>{
        public void Dump(ref string z, int d){
            z += new string('-', d * 2) + "frame\n";
            foreach(var b in this.Reverse()) b.Dump(ref z, d + 1);
        }
    }

    class Block : Dictionary<object, object>{
        public void Dump(ref string z, int d){
            z += new string('-', d * 2) + "block\n";
            foreach(var kv in this){
                z += new string('-', (d + 1) * 2)
                     + $"{kv.Key}: {kv.Value}\n";
            }
        }
    }

}}
