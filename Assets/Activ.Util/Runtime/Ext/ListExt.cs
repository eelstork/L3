using System.Collections;

namespace Activ.Util{
public static class ListExt{

    public static bool Empty(this IList self)
    => self == null || self.Count == 0;

}}
