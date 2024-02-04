namespace Activ.Util{
public static class StringExt{

    public static bool None(this string arg)
    => string.IsNullOrEmpty(arg);

    public static string _(this string arg)
    => string.IsNullOrEmpty(arg) ? null : (arg + ' ');

    public static string Tabs(
        this string arg, int count, int spaces = 4
    ) => new string(' ', count * spaces) + arg;

}}
