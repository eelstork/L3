using System; using System.Collections.Generic;

namespace Activ.Graphs{
public class BFS2{

    public T[] Find<T>(
        Func<T, T[]> graph, T _root, Func<T, bool> isGoal
    ){
        var visited = new HashSet<T>();
        var root = new N<T>(_root, parent: null);
        visited.Add(_root);
        var q = new Queue<N<T>>();
        q.Enqueue(root);
        while(q.Count > 0){
            var V = q.Dequeue();
            if(isGoal(V)){
                int i = 0; return Path(V, ref i);
            }
            foreach(var w in graph(V)){
                if(visited.Contains(w)) continue;
                var W = new N<T>(w, parent: V);
                visited.Add(w);
                q.Enqueue(W);
            }
        }
        return null;
        // ----------------------------------------
        T[] Path(N<T> x, ref int i){
            T[] p; if(object.ReferenceEquals(x, root)){
                p = new T[i + 1]; p[0] = root; i = 0;
            }else{
                ++i; p = Path(x.parent, ref i); p[++i] = x;

            } return p;
        }
    }

    class N<T>{
        public readonly N<T> parent;
        public readonly T value;
        public N(T z, N<T> parent){ value = z; this.parent = parent; }
        public static implicit operator T(N<T> node) => node.value;
    }

}}
