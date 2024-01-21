using System; using System.Collections.Generic;

public class BFS2{

    public T[] Find<T>(
        Func<T, T[]> graph, T _root, Func<T, bool> isGoal
    ){
        var visited = new Dictionary<T,N<T>>();
        var root = new N<T>(_root);
        visited[_root] = root;
        var q = new Queue<N<T>>();
        q.Enqueue(root);
        while(q.Count > 0){
            var V = q.Dequeue();
            if(isGoal(V)){
                int i = 0; return Path(V, ref i);
            }
            foreach(var w in graph(V)){
                if(visited.ContainsKey(w)) continue;
                var W = new N<T>(w);
                W.parent = V; visited[w] = W;
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
        public N<T> parent;
        public T value;
        public N(T arg) => value = arg;
        public static implicit operator T(N<T> node) => node.value;
    }

}
