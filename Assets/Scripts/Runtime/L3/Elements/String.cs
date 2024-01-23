namespace L3{
public partial class LString : Node, Expression{

    public string value = "";

    override public string TFormat() => "\"" + value + "\"";

}}
