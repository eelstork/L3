using InvOp = System.InvalidOperationException;
using UnityEngine;
using L3;

namespace R1{
public partial class Variable : L3.Node, Assignable, Named,
                                Accessible, ValueHolder{

    L3.Field owner;
    public object value;

    public string name => owner.name;

    public Variable(L3.Field owner){
        this.owner = owner;
    }

    object ValueHolder.value => value;

    public void Assign(object value)
    => this.value = value;

    public object Find(Node arg, Context cx){
        Debug.Log($"Find {arg} in {this} holding {value}");
        //throw new InvOp("Not imp");
        if(arg is L3.Var){
            var obj   = this.value;
            var type  = (obj as R1.Obj).GetClass();
            var @var  = arg as L3.Var;
            var name = @var.value;
            if(type.HasField(name)){
                return new R1.Obj.PropRef(name, obj as R1.Obj);
            }
            if(type.HasProp(name)){
                return new R1.Obj.PropRef(name, obj as R1.Obj);
            }
            throw new InvOp($"{@var.value} not in {obj}");
        }
        throw new InvOp($"Cannot find {arg} in {this}");
    }

    override public string ToString()
    => $"L3.Variable{{owner:{owner} value:{value}}}";

}}
