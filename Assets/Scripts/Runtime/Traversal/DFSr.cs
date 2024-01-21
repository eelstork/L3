using System; using System.Collections.Generic;

public class DFSr{

    public T[] Find<T>(
        Func<T, T[]> graph, T root, Func<T, bool> isGoal
    ){
        var visited = new HashSet<T>(); T[] path = null;
        return f(root); /* where */ T[] f(T x, int i = 0){
            foreach(var y in graph(x)){
                if(visited.Contains(y)) continue; visited.Add(y);
                if(isGoal(y)){
                    path = new T[i + 1]; path[i] = y; return path;
                }else{
                    path = f(y, i + 1);
                    if(path != null){ path[i] = y; return path; }
                }
            } return null;
        }
    }

}
