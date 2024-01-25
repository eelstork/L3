using System; using System.Collections.Generic;

namespace Activ.Graphs{
public class DFS{

    public T[] Find<T>(
        Func<T, T[]> graph, T root, Func<T, bool> isGoal
    ){
        var visited = new HashSet<T>();
        var parent = new Dictionary<T, T>();
        var q = new Stack<T>();
        visited.Add(root);
        q.Push(root);
        while(q.Count > 0){
            var v = q.Pop();
            if(isGoal(v)){
                int i = 0; return Path(v, ref i);
            }
            foreach(var w in graph(v)){
                if(visited.Contains(w)) continue;
                visited.Add(w);
                parent[w] = v;
                q.Push(w);
            }
        }
        return null;
        // ----------------------------------------
        T[] Path(T x, ref int i){
            T[] p; if(object.ReferenceEquals(x, root)){
                p = new T[i + 1]; p[0] = root; i = 0; return p;
            }else{
                ++i; p = Path(parent[x], ref i); p[++i] = x; return p;
            }
        }
    }

}}
