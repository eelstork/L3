using System.Collections.Generic;

public static class DictionaryExt{

    public static V GetValue<K, V>(this Dictionary<K, V> self, K key)
    => self.ContainsKey(key) ? self[key] : default(V);

    public static V GetValue<K, V>(
        this Dictionary<K, V> self, K key, V @default
    ) => self.ContainsKey(key) ? self[key] : @default;

}
