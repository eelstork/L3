using System.Collections.Generic;

namespace Activ.Util{
public static class DictionaryExt{

    public static V Get<K, V>(
        this Dictionary<K, V> dic, K key
    ){
        if(dic == null) return default(V);
        if(!dic.ContainsKey(key)) return default(V);
        return dic[key];
    }

}}
