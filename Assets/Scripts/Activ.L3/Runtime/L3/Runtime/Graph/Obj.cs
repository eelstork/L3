using L3;
using InvOp = System.InvalidOperationException;
using System.Collections.Generic;

namespace R1{
public class Obj{

    Class @class;
    public Dictionary<string, object> map;

    public Obj(Class @class){
        if(@class == null) throw new InvOp("Cannot be null");
        this.@class = @class;
    }

    public Class GetClass() => @class;

    public Node Find(string name){
        if(map.ContainsKey(name)){
            return map[name] as Node;
        }else{
            return null;
        }
    }

    public class BoundProp : Assignable{
        string name; Obj owner;
        public BoundProp(string name, Obj owner){
            this.name = name; this.owner = owner;
        }
        public void Assign(object value){
            if(owner.map == null) owner.map = new ();
            owner.map[name] = value;
        }
    }

}}
