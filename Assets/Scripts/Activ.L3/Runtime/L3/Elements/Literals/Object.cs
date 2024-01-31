using System.Reflection;
using InvOp = System.InvalidOperationException;
using UnityEngine;

namespace L3{
public partial class Object : Node, Expression, Assignable, Literal,
                              Accessible{

    FieldInfo field;
    object owner;

    public Object(FieldInfo f, object o){
        if(f == null) throw new InvOp("NULL field is not allowed");
        if(o == null) throw new InvOp("NULL target is not allowed");
        field = f;
        owner = o;
    }

    public object value => field.GetValue(owner);

    // <Assignable>
    public void Assign(object value){
        object unwrapped = value switch{
            L3.Object obj => obj.value,
            L3.Number num => num.value,
            L3.LString str => str.value,
            L3.Variable @var => @var.value,
            _ => throw new InvOp($"Don't know how to unwrap {value}")
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
    public object Find(Node arg, Context cx){
        Debug.Log($"Find {arg} in {this}");
        if(arg is Var){
            var obj   = this.value;
            var type  = obj.GetType();
            var @var  = arg as Var;
            var field = type.GetField(@var.value);
            return new Object(field, obj);
        }
        if(arg is L3.Call){
            var obj   = this.value;
            var @call  = arg as L3.Call;
            return R1.Call.Invoke(@call, cx, obj);
        }
        throw new InvOp($"Cannot find {arg} in {this}");
    }

    override public string TFormat() => "field " + field.Name;

}}
