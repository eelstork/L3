using System; using System.Collections;
using System.Collections.Generic;

namespace Activ.Util{
public static class ListExt{

    public static bool Empty(this IList self)
    => self == null || self.Count == 0;

    public static T MinBy<T>(this IEnumerable<T> self, Func<T, float> func){
        T @sel = default(T); var min = float.MaxValue;
        foreach(var x in self){
            var y = func(x); if(y < min){ @sel = x; min = y; }
        } return @sel;
    }

}}
