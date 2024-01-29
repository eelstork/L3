using System.Collections.Generic;

namespace L3{
public interface Context{

    Stack<Scope> stack{ get; }

    Scope scope{ get; }

    object Step(Node node);

    object Step(Node node, HashSet<Node> deps);

    object Instantiate(Class clss);

    void Log(string arg)
    => UnityEngine.Debug.Log(arg);

}}
