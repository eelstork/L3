using System; using System.Collections.Generic;
using System.Linq;
namespace Activ.Util{
public static class IEnumerableExt{

    public static string Format<T>(
        this IEnumerable<T> self, string sep = ", ",
        Func<T, string> fmt=null
    ){
        if(fmt == null) return string.Join(sep, self);
        else return string.Join(sep, self.Select( x => fmt(x) ));
    }

    public static string Formatnl<T>(
        this IEnumerable<T> self, Func<T, string> fmt=null
    ){
        if(fmt == null) return string.Join("\n", self);
        else return string.Join("\n", self.Select( x => fmt(x) ));
    }

}}
