namespace Activ.Util{
public static class StringExt{

    public static bool None(this string arg)
    => string.IsNullOrEmpty(arg);

    public static string _(this string arg)
    => string.IsNullOrEmpty(arg) ? null : (arg + ' ');

}}
