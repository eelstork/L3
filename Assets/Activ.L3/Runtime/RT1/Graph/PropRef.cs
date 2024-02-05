using System.Reflection;
using InvOp = System.InvalidOperationException;
using UnityEngine;

namespace R1{
public partial class PropRef : L3.Node, R1.Assignable, R1.Accessible{

    PropertyInfo prop;
    object owner;

    public PropRef(PropertyInfo p, object o){ prop = p; owner = o; }

    public object value => prop.GetValue(owner);

    // <Assignable>
    public void Assign(object value){
        // TODO suspicious unwrap call
        object unwrapped = value switch{
            FieldRef obj => obj.value,
            PropRef prop => prop.value,
            L3.Number num => num.value,
            L3.LString str => str.value,
            R1.Variable @var => @var.value,
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
    public object Find(L3.Node arg, Context cx){
        Debug.Log($"Find {arg} in {this}");
        if(arg is L3.Var){
            var obj   = this.value;
            var type  = obj.GetType();
            var @var  = arg as L3.Var;
            var field = type.GetField(@var.value);
            if(field != null) return new FieldRef(field, obj);
            var prop = type.GetProperty(@var.value);
            if(prop != null) return new PropRef(prop, obj);
            throw new InvOp($"{@var.value} not in {obj}");
        }
        if(arg is L3.Call){
            var obj   = this.value;
            var @call  = arg as L3.Call;
            return R1.Call.Invoke(@call, cx, obj);
        }
        throw new InvOp($"Cannot find {arg} in {this}");
    }

    //override public string TFormat(bool ex) => "prop " + prop.Name;

}}
