using InvOp = System.InvalidOperationException;
using UnityEngine;

namespace L3{
public partial class Variable : Node, Expression, Assignable, Named,
                                Accessible{

    L3.Field owner;
    public object value;

    public string name => owner.name;

    public Variable(L3.Field owner){
        this.owner = owner;
    }

    public void Assign(object value){
        switch(value){
            case Literal lit:
                this.value = lit.value;
                break;
            default: // TODO is this safe???
                this.value = value;
                break;
        }
    }

    public object Find(Node arg, Context cx){
        Debug.Log($"Find {arg} in {this} holding {value}");
        //throw new InvOp("Not imp");
        if(arg is Var){
            var obj   = this.value;
            var type  = (obj as R1.Obj).GetClass();
            var @var  = arg as Var;
            var name = @var.value;
            if(type.HasField(name)){
                return new R1.Obj.BoundProp(name, obj as R1.Obj);
            }
            if(type.HasProp(name)){
                return new R1.Obj.BoundProp(name, obj as R1.Obj);
            }
            throw new InvOp($"{@var.value} not in {obj}");
        }
        throw new InvOp($"Cannot find {arg} in {this}");
    }

}}
