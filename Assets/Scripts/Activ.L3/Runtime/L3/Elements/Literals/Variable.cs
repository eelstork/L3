using InvOp = System.InvalidOperationException;

namespace L3{
public partial class Variable : Node, Expression, Assignable, Named{

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

}}
