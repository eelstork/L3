using System.Collections.Generic;

namespace L3{
public class Env{

    Stack<Scope> stack = new ();
    R1.Obj @object;

    public void Enter(){
        stack.Clear();
        stack.Push( new () );
        @object = null;
    }

    public void Exit(){
        stack.Pop();
    }

    public void Push(Scope arg, object target){
        if(target is R1.Obj) @object = target as R1.Obj;
        stack.Push(arg);
    }

    public void Pop(){
        stack.Pop();
        @object = null;
    }

    public void Def(Node arg) => local.Add(arg);

    public Node FindVar(string name) => Find(name);

    public Node FindFunction(string name) => Find(name);

    public Node FindConstructor(string name) => Find(name);

    Node Find(string name)
    => local.Find(name) ?? @object?.Find(name);

    Scope local => stack.Peek();

}}
