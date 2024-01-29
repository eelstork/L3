namespace L3{
public class Field : Node, Dec, Expression{

    public string type = "", name="";

    override public string TFormat()
    => $"{name}: {type}";

    string Dec.name => name;

}}
