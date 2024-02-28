using System; using System.Collections;
using System.Collections.Generic;

namespace Activ.Util{
public static class ListExt{

    public static List<T> Clean<T>(this List<T> self){
        for(var i = self.Count - 1; i >= 0; i--){
            if(self[i] == null || self[i].Equals(null)){
                self.RemoveAt(i);
            }
        }
        return self;
    }

    public static bool Empty(this IList self)
    => self == null || self.Count == 0;

    public static T MinBy<T>(
        this IEnumerable<T> self, Func<T, float> func
    ){
        T @sel = default(T); var min = float.MaxValue;
        foreach(var x in self){
            if(x == null) continue;
            if(x.Equals(null)) continue;
            var y = func(x); if(y < min){ @sel = x; min = y; }
        } return @sel;
    }

    public static T MaxBy<T>(
        this IEnumerable<T> self, Func<T, float> func
    ){
        T @sel = default(T); var max = float.MinValue;
        foreach(var x in self){
            if(x == null) continue;
            if(x.Equals(null)) continue;
            var y = func(x); if(y > max){ @sel = x; max = y; }
        } return @sel;
    }

}}
