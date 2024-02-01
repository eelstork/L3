using System.Collections.Generic;

namespace L3{
public interface Context{

    Env env { get; }
    //Stack<Scope> stack{ get; }
    //Scope scope{ get; }

    object Step(Node node);

    object Ref(Node node);

    object Step(Node node, HashSet<Node> deps);

    object Instantiate(Class clss);

    void Log(string arg)
    => UnityEngine.Debug.Log(arg);

}}
