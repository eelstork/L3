using System.Collections.Generic;

namespace L3{
public interface Context{

    Stack<Scope> stack{ get; }

    Scope scope{ get; }

    object Step(Node node);

    void Log(string arg)
    => UnityEngine.Debug.Log(arg);

}}
