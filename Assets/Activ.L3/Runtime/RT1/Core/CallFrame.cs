using System.Collections.Generic;

namespace R1{
public class CallFrame : Stack<Scope>{

    R1.Obj @object;
    public object pose;

    public CallFrame(object pose){
        this.pose = pose ?? new Logger();
        //this.@object = target;
        //this.pose = pose;
        Push( new () );
    }

    public CallFrame(Scope arg){
        pose = new Logger();
        //this.@object = target;
        //this.pose = pose;
        Push(arg);
    }

    public CallFrame(Scope arg, Obj target){
        pose = new Logger();
        this.@object = target;
        //this.pose = null;
        Push(arg);
    }

    class Logger{
        public void Log(object arg) => UnityEngine.Debug.Log(arg);
    }

}}
