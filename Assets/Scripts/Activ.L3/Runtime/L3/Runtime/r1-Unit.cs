using System.Reflection;
using L3;

namespace R1{
public static class Unit{

    public static object Step(L3.Unit unit, Context cx){
        cx.Log("u/" + unit);
        cx.stack.Push(new Scope());
        foreach(var k in unit.nodes) cx.Step(k);
        cx.stack.Pop();
        return null;
    }

}}
