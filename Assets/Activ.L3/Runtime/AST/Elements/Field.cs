namespace L3{
public class Field : Node, Dec, Expression{

    public string type = "", name="";

    override public string TFormat(bool ex)
    => $"{name}: {type}";
    //=> $"L3Field{{{name}: {type}}}";

    string Dec.name => name;

}}
