using System; using System.Collections.Generic;
using InvOp = System.InvalidOperationException;
using Activ.Util;

namespace Activ.Graphs{
public class StaticSearch{

    public static T[] Find<T>(
        T root, Func<T, IEnumerable<T>> graph, Func<T, bool> isGoal,
        out int iter, Comparison<T> cmp=null, int maxIter = 24
    ){
        var visited = new HashSet<T>();
        var parent = new Dictionary<T, T>();
        var q = new List<T>();
        visited.Add(root);
        q.Enqueue(root);
        iter = 0;
        while(q.Count > 0){
            var v = q.Dequeue();
            if(isGoal(v)){
                int i = 0; return Path(v, ref i);
            }
            foreach(var w in graph(v)){
                if(w == null){
                    throw new InvOp($"Cannot add null to search (from {v})");
                }
                if(visited.Contains(w)) continue;
                visited.Add(w);
                parent[w] = v;
                q.Enqueue(w, cmp);
            }
            if(iter ++ >= maxIter) break;
        }
        return null;
        // ----------------------------------------
        T[] Path(T x, ref int i){
            T[] p; if(object.Equals(x, root)){
                p = new T[i + 1]; p[0] = root; i = 0; return p;
            }else{
                ++i; p = Path(parent[x], ref i); p[++i] = x; return p;
            }
        }
    }

}}
