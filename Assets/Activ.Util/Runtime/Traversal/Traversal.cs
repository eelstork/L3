using System; using System.Collections.Generic;

namespace Activ.Data{
public class Traversal{

    public static T[] FindPath<T>(
        T root, Func<T, IEnumerable<T>> graph, Func<T, bool> isGoal,
        int maxIter=-1
    ){
        Find(
            root, graph, isGoal, out T[] path,
            needsPath: true, maxIter: maxIter
        ); return path;
    }

    public static bool Find<T>(
        T root, Func<T, IEnumerable<T>> graph,
        Func<T, bool> isGoal, int maxIter=-1
    ) => Find(
        root, graph, isGoal, out T[] _, needsPath: false, maxIter
    );

    public static bool Find<T>(
        T root, Func<T, IEnumerable<T>> graph,
        Func<T, bool> isGoal,
        out T[] path, bool needsPath, int maxIter
    ){
        var visited = new HashSet<T>();
        var parent = new Dictionary<T, T>();
        var stack = new Stack<T>(); var rev = new Stack<T>();
        visited.Add(root); stack.Push(root); var iter = 0;
        while(stack.Count > 0 && (maxIter == -1 || iter++ < maxIter)){
            var v = stack.Pop();
            if(isGoal != null && isGoal(v)){
                if(needsPath){ int i = 0; path = Path(v, ref i); }
                else path = null;
            }
            var children = graph(v); if(children != null){
                foreach(var w in children) rev.Push(w);
                // NOTE maybe popping is faster than iterate and clear
                foreach(var w in rev){
                    if(visited.Contains(w)) continue;
                    visited.Add(w); parent[w] = v; stack.Push(w);
                }
            }
            rev.Clear();
        }
        path = null; return false;
        // ----------------------------------------
        T[] Path(T x, ref int i){
            T[] p; if(object.ReferenceEquals(x, root)){
                p = new T[i + 1]; p[0] = root; i = 0; return p;
            }else{
                ++i; p = Path(parent[x], ref i); p[++i] = x; return p;
            }
        }
    }

    public static T[] Traverse<T>(
        T root, Func<T, IEnumerable<T>> graph,
        Action<T> visit, Func<T, bool> isGoal = null, int maxIter = -1
    ){
        var visited = new HashSet<T>();
        var parent = new Dictionary<T, T>();
        var stack = new Stack<T>(); var rev = new Stack<T>();
        visited.Add(root); stack.Push(root); var iter = 0;
        while(stack.Count > 0 && (maxIter == -1 || iter++ < maxIter)){
            var v = stack.Pop(); visit(v);
            if(isGoal != null && isGoal(v)){
                int i = 0; return Path(v, ref i);
            }
            var children = graph(v); if(children != null){
                foreach(var w in children) rev.Push(w);
                // NOTE maybe popping is faster than iterate and clear
                foreach(var w in rev){
                    if(visited.Contains(w)) continue;
                    visited.Add(w); parent[w] = v; stack.Push(w);
                }
            }
            rev.Clear();
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
