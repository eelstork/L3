using System.Reflection;
using InvOp = System.InvalidOperationException;
using UnityEngine;
using L3;

namespace R1{
public class FieldRef : L3.Node, Assignable, Accessible{

    FieldInfo field;
    object owner;

    public FieldRef(FieldInfo f, object o){
        if(f == null) throw new InvOp("NULL field is not allowed");
        if(o == null) throw new InvOp("NULL target is not allowed");
        field = f;
        owner = o;
    }

    public object value => field.GetValue(owner);

    // <Assignable>
    public void Assign(object value){
        // TODO unwrapping should no longer be needed
        object unwrapped = value switch{
            R1.FieldRef obj => obj.value,
            L3.Number num => num.value,
            L3.LString str => str.value,
            R1.Variable @var => @var.value,
            _ => value
        };
        if(field.FieldType.IsAssignableFrom(typeof(int))){
            UnityEngine.Debug.Log($"type of unwrapped: {unwrapped.GetType()}");
            // TODO before performing this conversion we should know
            // the type of "unwrapped"
            unwrapped = (int)(float)unwrapped;
        }
        field.SetValue(owner, unwrapped);
    }

    // <Accessible>
    public object Find(L3.Node arg, Context cx){
        Debug.Log($"Find {arg} in {this}");
        if(arg is L3.Var){
            var obj   = this.value;
            var type  = obj.GetType();
            var @var  = arg as L3.Var;
            var field = type.GetField(@var.value);
            // TODO suspicious referencing
            return new FieldRef(field, obj);
        }
        if(arg is L3.Call){
            var obj   = this.value;
            var @call  = arg as L3.Call;
            return R1.Call.Invoke(@call, cx, obj);
        }
        throw new InvOp($"Cannot find {arg} in {this}");
    }

    //override public string TFormat(bool ex) => "field " + field.Name;

}}
