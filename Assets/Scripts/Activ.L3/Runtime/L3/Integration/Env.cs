using System.Collections.Generic;

namespace L3{
public class Env{

    Stack<Stack<Scope>> store = new ();
    R1.Obj @object;

    public void Enter(){
        store.Clear();
        var stack = new Stack<Scope>();
        stack.Push(new());
        store.Push(stack);
        @object = null;
    }

    public void Exit(){
        store.Pop();
    }

    public void PushBlock(){
        stack.Push( new () );
    }

    public void PopBlock(){
        stack.Pop();
    }

    public void PushCall(Scope arg, object target){
        if(target is R1.Obj) @object = target as R1.Obj;
        var stk = new Stack<Scope>();
        stk.Push(arg);
        store.Push(stk);
    }

    public void PopCall(){
        stack.Pop();
        @object = null;
    }

    public void Def(Node arg) => local.Add(arg);

    public Node FindVar(string name) => Find(name);

    public Node FindFunction(string name) => Find(name);

    public Node FindConstructor(string name) => Find(name);

    Node Find(string name){
        foreach(var z in stack){
            var output = z.Find(name);
            if(output != null) return output;
        }
        return @object?.Find(name);
    }

    Stack<Scope> stack => store.Peek();

    Scope local => stack.Peek();

}}
