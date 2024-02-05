namespace L3{
public partial class LString : Node, Expression, Literal{

    public string value = "";

    object Literal.value => value;

    override public string TFormat(bool ex) => "\"" + value + "\"";

    override public string ToString() => TFormat(false);

}}
