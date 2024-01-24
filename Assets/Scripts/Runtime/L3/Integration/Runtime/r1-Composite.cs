using System.Reflection;
using L3;

namespace R1{
public static class Composite{

    // TODO BT style composites not implemented
    public static object Step(L3.Composite co, Script cx){
        cx.Log("co/" + co);
        foreach(var k in co.nodes){
            cx.Step(k as Node);
        }
        return null;
    }

}}
