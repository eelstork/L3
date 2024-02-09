using System.Collections.Generic;
using InvOp = System.InvalidOperationException;
using Method = System.Reflection.MethodInfo;
using Activ.Util; using L3;

namespace R1{
public class Env{

    Stack<CallFrame> store = new (); R1.Obj @object;

    public object pose
    { get => frame.pose; set => frame.pose = value; }

    public void Enter(object pose){
        store.Clear();
        store.Push(new CallFrame( pose ));
        @object = null;
    }

    public void Exit() => store.Pop();

    public void EnterScope() => frame.Push( new () );

    public void ExitScope() => frame.Pop();

    public void EnterCall(Scope arg, object target)
    => store.Push( new CallFrame(arg, target as R1.Obj) );

    public void ExitCall(){ store.Pop(); @object = null; }

    // ------------------------------------------------------------

    public void Def(Node arg) => local.Add(arg);

    // ------------------------------------------------------------

    public object CallFunction(
        object target, string name, object[] args, Context cx
    ){
        target = target ?? frame.pose;
        var l3func = frame.FindL3Func(target, name, args);
        if(l3func != null) return CallL3Func(
            target, l3func, args, cx
        );
        var csfunc = frame.FindCsFunc(target, name, args);
        if(csfunc != null) return CallCsFunc(
            target, csfunc, args
        );
        throw new InvOp("Function not found: " + name);
    }

    object CallL3Func(
        object target, Function func, object[] args, Context cx
    ){
        var sub = new Scope();
        // Load arguments into the subscope
        for(int i = 0; i < args.Length; i++){
            var arg = new Arg(func.parameters[i].name, args[i]);
            sub.Add(arg);
        }
        EnterCall(sub, target);
        //Debug.Log($"CALL simple function: [{node}]");
        var content = func.expression as Node;
        object output;
        if(content != null){
            output = cx.Step(content);
        }else{
            if(func.auto){
                return Solver.Find(func.type, args);
            }
            output = Token.@void;
        }
        // Exit subscope and return the output
        ExitCall();
        return output;
    }

    object CallCsFunc(object target, Method[] group, object[] args){
        var output = CSharp.Invoke(group, args, target);
        if(output.type.Equals(typeof(void))){
            return Token.@void;
        }else{
            return output.value;
        }
    }

    public Node FindConstructor(string name) => Find(name);

    public object GetVariableValue(string @var, bool opt)
    => frame.GetValue(@var, opt);

    public object Reference(string @var, bool opt)
    => frame.Reference(@var, opt);

    // -------------------------------------------------------------

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

    // TODO remove if possible
    Node Find(string name){
        foreach(var z in frame){
            var output = z.Find(name);
            if(output != null) return output;
        }
        return @object?.Find(name);
    }

    CallFrame frame => store.Peek();

    Scope local => frame.Peek();

}}
