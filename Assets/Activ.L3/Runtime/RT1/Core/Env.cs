using System.Collections.Generic;
using L3;

namespace R1{
public class Env{

    Stack<Stack<Scope>> store = new ();
    R1.Obj @object;

    public void Enter(){
        store.Clear();
        var frame = new Stack<Scope>();
        frame.Push(new());
        store.Push(frame);
        @object = null;
    }

    public void Exit(){
        store.Pop();
    }

    public void EnterScope(){
        //og($"ENTER SCOPE => {frame.Count}");
        frame.Push( new () );
    }

    public void ExitScope(){
        //og($"EXIT SCOPE => {frame.Count}");
        frame.Pop();
    }

    public void EnterCall(Scope arg, object target){
        if(target is R1.Obj) @object = target as R1.Obj;
        var stk = new Stack<Scope>();
        stk.Push(arg);
        store.Push(stk);
    }

    public void ExitCall(){
        store.Pop();
        @object = null;
    }

    public void Def(Node arg) => local.Add(arg);

    public Node FindVar(string name) => Find(name);

    public Node FindFunction(string name) => Find(name);

    public Node FindConstructor(string name) => Find(name);

    Node Find(string name){
        foreach(var z in frame){
            var output = z.Find(name);
            if(output != null) return output;
        }
        return @object?.Find(name);
    }

    public void Dump(){
        var str = "";
        var i = 0; foreach(var frame in store){
            str += $"FRAME #{i++}\n";
            Dump(frame);
        }
        void Dump(Stack<Scope> frame){
            var j = 0; foreach(var scope in frame){
                str += $"-- SCOPE #{j++}";
                var nodes = scope._nodes;
                if(nodes == null || nodes.Count == 0){
                    str += " (empty)";
                }else foreach(var node in scope._nodes)
                    str += $"\n---- {node}";
            }
        }
        UnityEngine.Debug.Log("STORE\n" + str);
    }

    Stack<Scope> frame => store.Peek();

    Scope local => frame.Peek();

    void Log(object arg){}
    //=> UnityEngine.Debug.Log(arg);

}}
