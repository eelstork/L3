namespace L3{
public partial class Var : Node, Expression{

    public string value = "";
    public bool opt = false;

    override public string TFormat() => value;

}}
