namespace L3{
public class Class : AbstractBranch<Node>, Dec, Named{

    public string name = "";

    override public string TFormat()
    => $"class {name}";

    string Named.name => name;

    string Dec.name => name;

    public bool HasField(string name)
    => System.Array.Exists(children, x => {
        var field = x as L3.Field;
        if(field == null) return false;
        return field.name == name;
    });

    public bool HasProp(string name) => false;

    [EditorAction]
    public void AddField()
    => AddChild(new Field());

    [EditorAction]
    public void AddFunction()
    => AddChild(new Function());

    override public string ToString()
    => $"L3.Class{{{name}}}";

}}
