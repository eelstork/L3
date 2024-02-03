using System.Collections.Generic;
using Activ.Util;

namespace L3{
public partial class Function : Branch, Dec, Named{

    public bool auto;
    public string type = "void";
    public string name = "";
    public List<Parameter> parameters;
    [Hierarchy]
    public Node expression;

    public Function(){
        name = "Bar";
    }

    string Named.name => name;

    string Dec.name => name;

    override public Node[] children
    => expression == null ? null : new Node[]{ expression as Node };

    override public void AddChild(Node child)
    => expression = child;

    override public void DeleteChild(Node child){
        expression = null;
    }

    public Function(string name){
        this.name = name;
    }

    override public string TFormat(){
        var str = auto.@as("auto") + type._() + name;
        if(parameters == null)
            return str + "()";
        else
            return str + "(" + string.Join(", ", parameters) + ")";
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

    override public string ToString() => TFormat();

}}
