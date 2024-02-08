namespace Activ.Util{
public static class StringExt{

    public static bool None(this string self)
    => string.IsNullOrEmpty(self);

    public static string _(this string self)
    => string.IsNullOrEmpty(self) ? null : (self + ' ');

    public static string Tabs(
        this string self, int count, int spaces = 4
    ) => new string(' ', count * spaces) + self;

}}
