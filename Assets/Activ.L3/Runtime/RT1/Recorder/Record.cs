using System;
using System.Collections.Generic;
using UnityEngine;
using static L3.Statuses;
using Node = L3.Node;

namespace R1{
public partial class Record : TNode{

    public Frame frame;
    public readonly DateTime date;
    public readonly float time;

    public TNode[] children => new TNode[]{ frame };

    public Record(){
        date = DateTime.Now;
        time = Time.time;
    }

    public void Enter(Node arg){
        var @new = new Frame(arg, parent: frame);
        if(frame != null) frame.Add(@new);
        frame = @new;
    }

    public void Exit(Node arg, object value){
        frame.value = value;
        if(frame.parent != null) frame = frame.parent;
    }

    public void ExitWithError(Node arg, System.Exception err){
        frame.error = err;
        if(frame.parent != null) frame = frame.parent;
    }

    public bool Matches(string arg, object[] args) => false;

}}
