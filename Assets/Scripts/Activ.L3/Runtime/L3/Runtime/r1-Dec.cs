using System.Reflection;
using L3;
using InvOp = System.InvalidOperationException;

namespace R1{
public static class Dec{

    public static object Step(L3.Dec dec, Context cx){
        cx.Log("dec/" + dec);
        if(!(dec is Node)){
            throw new InvOp($"Not a node: {dec}");
        }
        if(cx.scope == null){
            throw new InvOp($"No current scope");            
        }
        cx.scope.Add(dec as Node);
        return null;
    }

}}
