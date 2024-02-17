using System;

namespace Activ.Util.Rec{
public partial class Record<T>{

    public Frame<T> frame;

    public void Enter(T arg){
        var @new = new Frame<T>(arg, parent: frame);
        if(frame != null) frame.Add(@new);
        frame = @new;
    }

    public void Exit(T arg, object value){
        frame.value = value;
        if(frame.parent != null) frame = frame.parent;
    }

    public void ExitWithError(T arg, Exception err){
        frame.error = err;
        if(frame.parent != null) frame = frame.parent;
    }

}}
