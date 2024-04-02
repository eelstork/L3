using System; using System.Collections;
using System.Collections.Generic;
using System.Linq;
using InvOp = System.InvalidOperationException;

namespace Activ.Util{
public static class ListExt{

    public static T Any<T>(
        this IList<T> self, bool allowDefault=false
    ) => allowDefault && ((IList)self).Empty() ? default(T)
    : self[UnityEngine.Random.Range(0, self.Count)];

    public static T Any<T>(
        this IList<T> self, Predicate<T> cond,
        bool allowDefault=false
    ) => self.Where( x => cond(x) ).ToList().Any();

    public static List<T> Clean<T>(this List<T> self){
        for(var i = self.Count - 1; i >= 0; i--){
            if(self[i] == null || self[i].Equals(null)){
                self.RemoveAt(i);
            }
        }
        return self;
    }

    public static T Dequeue<T>(this List<T> self){
        var n = self.Count - 1;
        var e = self[n];
        self.RemoveAt(n);
        return e;
    }

    // Insert an element at the beginning (queue-style)
    public static void Enqueue<T>(this List<T> self, T arg)
    => self.Insert(0, arg);

    // Insert an element in sorted order.
    // (x, y) => x.CompareTo(y) will sort in ascending order, whereas
    // (x, y) => y.CompareTo(x) will sort in descending order
    public static void Enqueue<T>(
        this List<T> self, T arg, Comparison<T> comparer
    ){
        if(comparer == null){ self.Insert(0, arg); return; }
        var n = self.Count;
        for(var i = n - 1; i >= 0; i--){
            var val = comparer(arg, self[i]);
            if(val > 0){ self.Insert(i + 1, arg); return; }
        } self.Insert(0, arg);
    }

    public static string Format<T>(this List<T> self){
        var str = string.Join(", ", self);
        return "{" + str + "}";
    }

    public static List<T> FindAllOrDefault<T>(
        this List<T> self, Predicate<T> cond
    ){
        var @out = self.FindAll(cond);
        return @out.Count == 0 ? null : @out;
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
