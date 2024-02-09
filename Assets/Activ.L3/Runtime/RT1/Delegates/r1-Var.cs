using System.Reflection;
using InvOp = System.InvalidOperationException;
using L3;

namespace R1{
public static class Var{

    public static object Resolve(L3.Var @var, Context cx)
    => cx.env.GetVariableValue(@var.value, @var.opt);

    public static object Refer(L3.Var @var, Context cx)
    => cx.env.Reference(@var.value, @var.opt);

}}
