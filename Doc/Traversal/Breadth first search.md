# Breadth first search

First let's have a look at a breadth first search; in this case the function signature for a generic search may be something like:

```cs
public T[] Find<T>(
    Func<T, T[]> graph,
    T root,
    Func<T, bool> isGoal
);
```

There are implied limitations to how efficient this can be, which are as follows:

(1) If the `graph` implementation must re-create a T[] at every query, this causes significant overheads.
(2) The function does not assume nodes may be labeled. The main overhead associated with that is, we then need a set to store visited nodes. This overhead can be discounted (or reduced) if we know the graph is acyclic, or have a list of the considered nodes (which then implies searching a finite subgraph)
