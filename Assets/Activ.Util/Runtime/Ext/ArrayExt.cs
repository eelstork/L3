using Random = UnityEngine.Random;
using System.Collections.Generic;

namespace Activ.Util{
public static class ArrayExt{

    public static T Any<T>(this T[] self)
    => self[Random.Range(0, self.Length)];

}}
