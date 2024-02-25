using UnityEngine;

namespace Activ.Util{
public static class StringExt{

    public static string AddWord(this string x, string y){
        if(x.None()) return y;
        if(y.None()) return x;
        return x + ' ' + y;
    }

    public static string AddSQuoted(this string x, string y){
        if(x.None()) return y.None() ? null : y.SQuoted();
        if(y.None()) return x;
        return x + ' ' + y.SQuoted();
    }

    public static char Any(this string self)
    => self[Random.Range(0, self.Length)];

    public static char AnyToUpper(this string self)
    => char.ToUpper(self[Random.Range(0, self.Length)]);

    public static string DQuoted(this string x)
    => "\"" + x + "\"";

    public static string SQuoted(this string x)
    => "'" + x + "'";

    public static bool None(this string self)
    => string.IsNullOrEmpty(self);

    public static string _(this string self)
    => string.IsNullOrEmpty(self) ? null : (self + ' ');

    public static string Tabs(
        this string self, int count, int spaces = 4
    ) => new string(' ', count * spaces) + self;

}}
