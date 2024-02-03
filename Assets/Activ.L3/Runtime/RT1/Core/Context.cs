using System.Collections.Generic;
using L3;

namespace R1{
public interface Context{

    Env env { get; }
    object pose { get; set; }

    Node FindFunction(string name);

    object Instantiate(Class clss);

    object Ref(Node node);

    object Step(Node node);

    object Step(Node node, HashSet<Node> deps);

    void Log(string arg)
    => UnityEngine.Debug.Log(arg);

}}
