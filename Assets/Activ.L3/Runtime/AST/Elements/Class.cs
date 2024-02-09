using Activ.Util;

namespace L3{
public class Class : AbstractBranch<Node>, Dec, Named{

    public string name = "";

    override public string TFormat(bool ex)
    => $"class {name}";

    string Named.name => name;

    string Dec.name => name;

    public Function FindConstructor(int paramcount){
        UnityEngine.Debug.Log(
            $"Find {paramcount}-param constructor for {name} (nodes {nodes})"
        );
        return (Function) FindNode( Match );
        bool Match(Node arg){
            var func = arg as Function;
            return func != null && func.type.None()
                                && func.paramcount == paramcount;
        }
    }

    public bool HasField(string name)
    => System.Array.Exists(children, x => {
        var field = x as L3.Field;
        if(field == null) return false;
        return field.name == name;
    });

    public bool HasProp(string name) => false;

    override public string ToString() => $"L3.Class{{{name}}}";

    // -------------------------------------------------------------

    [eda] public void AddField   () => AddChild(new Field());
    [eda] public void AddFunction() => AddChild(new Function());

}}
