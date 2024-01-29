using System.Reflection;
using InvOp = System.InvalidOperationException;
using UnityEngine;

namespace L3{
public partial class BoundProp : Node, Expression, Assignable, Literal,
                                 Accessible{

    PropertyInfo prop;
    object owner;

    public BoundProp(PropertyInfo p, object o){ prop = p; owner = o; }

    public object value => prop.GetValue(owner);

    // <Assignable>
    public void Assign(object value){
        object unwrapped = value switch{
            L3.Object obj => obj.value,
            L3.BoundProp prop => prop.value,
            L3.Number num => num.value,
            L3.LString str => str.value,
            L3.Variable @var => @var.value,
            Vector3 v3 => v3,
            _ => throw new InvOp($"Don't know how to unwrap {value}")
        };
        if(prop.PropertyType.IsAssignableFrom(typeof(int))){
            UnityEngine.Debug.Log($"type of unwrapped: {unwrapped.GetType()}");
            // TODO before performing this conversion we should know
            // the type of "unwrapped"
            unwrapped = (int)(float)unwrapped;
        }
        prop.SetValue(owner, unwrapped);
    }

    // <Accessible>
    public object Find(Node arg, Context cx){
        Debug.Log($"Find {arg} in {this}");
        if(arg is Var){
            var obj   = this.value;
            var type  = obj.GetType();
            var @var  = arg as Var;
            var field = type.GetField(@var.value);
            if(field != null) return new Object(field, obj);
            var prop = type.GetProperty(@var.value);
            if(prop != null) return new BoundProp(prop, obj);
            throw new InvOp($"{@var.value} not in {obj}");
        }
        if(arg is L3.Call){
            var obj   = this.value;
            var @call  = arg as L3.Call;
            return R1.Call.Invoke(@call, cx.scope, cx, obj);
        }
        throw new InvOp($"Cannot find {arg} in {this}");
    }

    override public string TFormat() => "prop " + prop.Name;

}}
