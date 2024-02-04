## Stack design

A simple stack. This stack is designed to support the following behaviors:

(1) We can always define and query variables
(2) When entering a function, variables defined by the calling function are no longer visible
(3) When entering a block, variables inside the parent block or frame are still visible.
(4) When exiting a function or block, variables defined inside the function or block are discarded.

```cs
public class Stack0{

    Stack<Frame> s = new ();

    public Stack0(){ EnterFrame(); EnterBlock(); }

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

    Frame f => s.Peek();
    Block b => f.Peek();

    class Frame : Stack<Block>{}
    class Block : Dictionary<object, object>{}

}
```

At first glance this implementation may look flawed. When overriding a variable inside a black, then looking up the same, do we get a value from the parent block? However that is not the case. Iterating a stack goes last to first, similar to repeatedly popping the stack.

Another issue, which may be dismissed as an implementation quirk, but does feel grating: we can only define variables inside blocks, not frames or the stack itself. This becomes visible when dumping the stack. Since this quirk does not sit, let's refactor.
