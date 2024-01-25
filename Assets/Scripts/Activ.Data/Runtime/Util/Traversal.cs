using UnityEngine;
using System; using System.Collections.Generic;

namespace Activ.Data{
public class Traversal{

    const int MaxIter = 32;

    public static T[] Traverse<T>(
        T root,
        Action<T> visit,
        Func<T, T[]> graph,
        Func<T, bool> isGoal = null
    ){
        var visited = new HashSet<T>();
        var parent = new Dictionary<T, T>();
        var q = new Queue<T>();
        visited.Add(root);
        q.Enqueue(root);
        int iter = 0;
        while(q.Count > 0 && iter++ < MaxIter){
            var v = q.Dequeue();
            visit(v);
            if(isGoal != null && isGoal(v)){
                int i = 0; return Path(v, ref i);
            }
            if(q.Count < 3) foreach(var w in graph(v)){
                if(visited.Contains(w)) continue;
                visited.Add(w);
                parent[w] = v;
                q.Enqueue(w);
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
