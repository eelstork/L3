namespace L3{
public class Parameter{

    public string type;
    public string name;

    public Parameter(){
        type = "TYPE";
        name = "NAME";
    }

    override public string ToString(){
        return type + " " + name;
    }

}}
