namespace L3{
public partial class Number : Node, Expression, Literal{

    public float value;

    object Literal.value => value;

    override public string TFormat() => value.ToString();

    public static implicit operator int(Number self)
    => (int)self.value;

    public static implicit operator float(Number self)
    => self.value;

    public static implicit operator double(Number self)
    => self.value;

}}
