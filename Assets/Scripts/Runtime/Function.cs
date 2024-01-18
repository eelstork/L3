using System.Collections.Generic;

[System.Serializable] public partial class Function : Branch{

    public List<Parameter> parameters;
    [Hierarchy]
    public Expression expression;

    public Function(){
        name = "Bar";
    }

    override public Node[] children
    => expression == null ? null : new Node[]{ expression as Node };

    public Function(string name){
        this.name = name;
    }

    [EditorAction]
    public void AddParameter(){
        if(parameters == null) parameters = new ();
        parameters.Add(new ());
    }

    [EditorAction]
    public void UseComposite(){
        expression = new Composite();
    }

    [EditorAction]
    public void UseCall(){
        expression = new Call();
    }

}
