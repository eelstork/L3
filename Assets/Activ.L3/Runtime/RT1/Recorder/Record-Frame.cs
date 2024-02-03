using System;
using System.Collections.Generic;
using UnityEngine;
using static L3.Statuses;
using Node = L3.Node;
using static UnityEngine.Debug;
using System.Linq;

namespace R1{
public partial class Record : TNode{

    public class Frame : TNode{
        public Frame parent;
        public List<Frame> children;
        public Node node;
        public System.Exception error;
        public object value;
        public int depth;

        public Frame(Node x, Frame parent){
            this.node = x; this.parent = parent;
            depth = parent == null ? 0 : parent.depth + 1;
        }
        public void Add(Frame child){
            if(children == null) children = new ();
            children.Add(child);
        }
        override public string ToString()
        => ValueChar() + (' ' + node.ToString()) + " → " + ValueToString();

        string ValueToString()
        => value == null ? "null" :  value.ToString();

        public char ValueChar(){
            if(error != null) return 'E';
            if(value == null) return '-';
            if(IsCont(value)) return '→';
            if(IsDoneStatus(value)) return '✓';
            if(IsFailing(value)) return '✗';
            return '+';
        }

        TNode[] TNode.children => children?.ToArray();

        public bool Matches(string arg, object[] args){
            var call = node as L3.Call;
            if(call == null) return false;
            if(call.function == arg){
                if(args.Length != childCount) return false;
                for(var i = 0; i < args.Length; i++){
                    if(!args[i].Equals(children[i].value)){
                        Log("arg mismatch");
                        return false;
                    }
                }
                var pargs = (
                    from x in children select x.value
                ).ToArray();
                var pargstr = string.Join(", ", pargs);
                var argstr = string.Join(", ", args);
                Log($"History did match [{arg}]");
                if(IsDoneStatus(value)){
                    Log($"Prev call was done: {value}");
                    return true;
                }else{
                    Log($"Prev call was not done: {value}");
                    return false;
                }
            }
            return false;
        }

        int childCount => children == null ? 0 : children.Count;

    }

}}
