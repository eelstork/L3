using System.Collections.Generic;

public class Scope{

    List<Node> nodes = new ();

    public void Add(Node arg){
        nodes.Add(arg);
    }

    public Node Find(string name) => null;

}
