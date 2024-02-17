using System;
using Node = System.Object;

namespace Activ.Util.Rec{
public partial class Record{

    public Frame frame;

    public void Enter(Node arg){
        var @new = new Frame(arg, parent: frame);
        if(frame != null) frame.Add(@new);
        frame = @new;
    }

    public void Exit(Node arg, object value){
        frame.value = value;
        if(frame.parent != null) frame = frame.parent;
    }

    public void ExitWithError(Node arg, Exception err){
        frame.error = err;
        if(frame.parent != null) frame = frame.parent;
    }

}}
