using static UnityEngine.Debug;

namespace Activ.LTP{
public class IO<Src, Dst, A, K, V, N>{

    Reader<Src, A, K, V, N> reader;
    Writer<Dst, K, V, N> writer;

    public IO(Reader<Src, A, K, V, N> r, Writer<Dst, K, V, N> w)
    { reader = r; writer = w; }

    public Dst Convert(Src src){
        var dst = writer.Materialize(
            reader.Id(src), under: default(Dst)
        );
        Convert(src, dst);
        return dst;
    }

    public void Convert(Src arg, Dst parent){
        //og($"Convert {arg} => {parent}");
        var props = reader.Props(arg);
        if(props != null) foreach(var prop in props){
            var kv = reader.Map(prop);
            writer.Assign(kv.key, kv.value, parent);
        }
        var children = reader.Children(arg);
        if(children != null) foreach(var child in children) Convert(
            child, writer.Materialize(reader.Id(child), under: parent)
        );
    }

}

public interface Reader<T, A, K, V, N>{
    N Id(T @object);
    (K key, V value) Map(A arg);
    A[] Props(T @object);
    T[] Children(T parent);
}

public interface Writer<T, K, V, N>{

    T Materialize(N id, T under);
    void Assign(K key, V value, T obj);

}

}  // Activ.LTP
