namespace L3{
public partial class Object : Node, Expression, Literal{

    public object value;

    public Object(object value) => this.value = value;

    object Literal.value => value;

    override public string TFormat() => value.ToString();

}}
