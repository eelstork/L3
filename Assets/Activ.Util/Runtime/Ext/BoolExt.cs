namespace Activ.Util{
public static class BoolExt{

    public static string @as(this bool x, string label)
    => x ? (label + ' ') : null;

}}
