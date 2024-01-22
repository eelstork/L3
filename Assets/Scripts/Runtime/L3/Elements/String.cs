namespace L3{
public partial class String : Node, Expression{

    public string value = "";

    override public string TFormat() => "\"" + value + "\"";

}}
