namespace L3{
public class Class : AbstractBranch<Node>, Dec{

    public string name = "";

    override public string TFormat()
    => $"class {name}";

    string Dec.name => name;

    [EditorAction]
    public void AddField()
    => AddChild(new Field());

    [EditorAction]
    public void AddFunction()
    => AddChild(new Function());

}}
