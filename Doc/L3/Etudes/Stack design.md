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

```cs
public class Stack : Stack<Stack.Scope>{

    public Stack() => EnterFrame();

    public void EnterFrame() => Push( new Scope(clear: false) );
    public void ExitFrame () => Pop();
    public void EnterBlock() => Push( new Scope(clear: true) );
    public void ExitBlock () => Pop();

    public bool Exists(object key) => Find(key, lenient: true).found;

    public object this[object key]{
        get => Find(key, lenient: false).@out;
        set => Peek()[key] = value;
    }

    public (object @out, bool found) Find(object key, bool lenient){
        foreach(var scope in this){
            if(scope.ContainsKey(key)) return (scope[key], true);
            if(!scope.clear) break;
        } return lenient ? (null, false)
                         : throw new InvOp($"Not found: {key}");
    }

    public class Scope : Dictionary<object, object>{
        public readonly bool clear;
        public Scope(bool clear) => this.clear = clear;
    }

}
```

# Assuming a target object

When we call functions, usually we have access to more than just local variables. In other words, we have an environment - be it an object, or a kind of global thing. I will assume that this is an object, and the target of a function call.

For now let's make no further assumptions about this object; instead we'll assume a helper object ("Map") knows how to access the target object.

- Assuming that entering frames can assign the target object, whereas entering block does not. The target object of the current frame remains visible from inside a block.
- Assuming the `Map` helper can handle null; this is because we may
wish for language level resources; names which are always available

```cs
public class Stack : Stack<Stack.Scope>{

    Map map;

    public Stack(Map map=null){ this.map = map; EnterFrame(); }

    public void EnterFrame(object target=null)
    => Push( new(target, clear: false));
    public void ExitFrame() => Pop();
    public void EnterBlock() => Push(new (null, clear: true));
    public void ExitBlock() => Pop();
    public bool Exists(object key) => Find(key, lenient: true).found;

    public object this[object key]{
        get => Find(key, lenient: false).@out;
        set => Peek()[key] = value;
    }

    (object @out, bool found) Find(object key, bool lenient){
        foreach(var scope in this){
            if(scope.ContainsKey(key)) return (scope[key], true);
            if(map != null){
                var z = map.Find(scope.target, key, out bool found);
                if(found) return (z, true);
            }
            if(!scope.clear) break;
        } return lenient ? (null, false)
                         : throw new InvOp($"Not found: {key}");
    }

    public class Scope : Dictionary<object, object>{
        public readonly bool clear;
        public readonly object target;
        public Scope(object target, bool clear){
            this.clear = clear; this.target = target;
        }
        override public string ToString(){
            var str = clear ? "block" : "frame";
            if(target != null) str += '(' + target.ToString() + ')';
            return str;
        }
    }

}
```
