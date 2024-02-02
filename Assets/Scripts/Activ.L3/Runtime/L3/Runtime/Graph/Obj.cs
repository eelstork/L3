using L3;
using InvOp = System.InvalidOperationException;
using System.Collections.Generic;
using System.Linq;

namespace R1{
public class Obj : Accessible{

    Class @class;
    public Dictionary<string, object> map;

    public Obj(Class @class){
        if(@class == null) throw new InvOp("Cannot be null");
        this.@class = @class;
    }

    public Class GetClass() => @class;

    public object Find(Node arg, Context cx){
        var @variable = (L3.Var)arg;
        return Find(@variable.value);
    }

    public object Ref(Node arg, Context cx){
        var @variable = (L3.Var)arg;
        return new PropRef(@variable.value, this);
    }

    public Node Find(string name){
        if(map.ContainsKey(name)){
            return map[name] as Node;
        }else{
            return null;
        }
    }

    public class PropRef : Assignable{
        string name; Obj owner;
        public PropRef(string name, Obj owner){
            this.name = name; this.owner = owner;
        }
        public void Assign(object value){
            if(owner.map == null) owner.map = new ();
            owner.map[name] = value;
        }
    }

    override public string ToString(){
        return @class.name + "{ " + FieldsToString() + " }";
    }

    string FieldsToString(){
        if(map == null) return null;
        return string.Join(
            ", ", from kv in map select $"{kv.Key}: {kv.Value}"
        );
    }

}}
