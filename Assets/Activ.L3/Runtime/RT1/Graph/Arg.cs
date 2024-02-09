namespace R1{
public class Arg : L3.Node, Named, ValueHolder{

    public string name;
    public object value;

    string Named.name => name;

    public Arg(string name, object val){
        this.name = name;
        this.value = val;
    }

    object ValueHolder.value => value;

}}
