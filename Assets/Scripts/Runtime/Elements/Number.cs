public partial class Number : Node, Expression{

    public float value;

    override public string TFormat() => value.ToString();

}
