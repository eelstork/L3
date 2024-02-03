using System; using System.Collections.Generic;
using InvOp = System.InvalidOperationException;

namespace Activ.Graphs{
public class BFS1{

    public static T[] Find<T>(
        Func<T, T[]> graph, T root, Func<T, bool> isGoal,
        int maxIter = 24
    ){
        var visited = new HashSet<T>();
        var parent = new Dictionary<T, T>();
        var q = new Queue<T>();
        visited.Add(root);
        q.Enqueue(root);
        var iter = 0;
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
                q.Enqueue(w);
            }
            if(iter ++ >= maxIter) break;
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
