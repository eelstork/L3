using System.Collections.Generic;

namespace L3{
public interface Context{

    Env env { get; }

    Node FindFunction(string name);

    object Instantiate(Class clss);

    object Ref(Node node);

    object Step(Node node);

    object Step(Node node, HashSet<Node> deps);


    void Log(string arg)
    => UnityEngine.Debug.Log(arg);

}}
