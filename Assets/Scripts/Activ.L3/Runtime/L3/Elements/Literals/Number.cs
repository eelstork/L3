namespace L3{
public partial class Number : Node, Expression, Literal{

    public float value;

    object Literal.value => value;

    override public string TFormat() => value.ToString();

}}
