using System.Collections.Generic;
using L3;

namespace R1{
public interface Context{

    Env env { get; }
    History history { get; }
    object pose { get; set; }

    object CallFunction(object target, string name, object[] args);

    object Instantiate(Class clss);

    object Ref(Node node);

    object Step(Node node);

    object Step(Node node, HashSet<Node> deps);

    void Log(string arg)
    => UnityEngine.Debug.Log(arg);

    void SetIndex(L3.Composite arg, int i);
    int GetIndex(L3.Composite arg);

}}
