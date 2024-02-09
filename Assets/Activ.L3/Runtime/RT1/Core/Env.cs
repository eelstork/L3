using System.Collections.Generic;
using L3;

namespace R1{
public class Env{

    Stack<CallFrame> store = new ();
    R1.Obj @object;

    public object pose{
        get => frame.pose; set => frame.pose = value;
    }

    public void Enter(object pose){
        store.Clear();
        store.Push(new CallFrame( pose ));
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

    public void EnterCall(Scope arg, object target)
    => store.Push( new CallFrame(arg, target as R1.Obj) );

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
            // TODO its value may be null
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

    CallFrame frame => store.Peek();

    Scope local => frame.Peek();

    void Log(object arg){}
    //=> UnityEngine.Debug.Log(arg);

}}
