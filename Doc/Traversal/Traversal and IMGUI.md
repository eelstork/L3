# Traversal and IMGUI

To get started with this I pick up my reference search implementation. The signature is like this:

```cs
public T[] Find<T>(
    Func<T, T[]> graph, T root, Func<T, bool> isGoal
);
```

Now, in user interface automation the traversal is meaningful since we are rendering an interactive hierarchy of objects. Superficially it seems like some of the functionality is not useful (it's a search!), and some functionality is lacking:

(1) We do not need the `isGoal` function, right?

(2) The `graph` function can be used to return children of the current node, however we'd like to *visit* nodes. In this case visiting a node will mean calling IMGUI to render widgets.

(3) In some cases `visit` would break down into `enter` and `exit`. This will make sense for example if we render the nodes as text with brackets:

On enter: draw "myObj{"
On exit:  draw "}"

As a case in point we also would like to track depth for indenting. So it seems like instead of `Visit(T node)` a call Visit(T node, Stack<T> path) would be helpful.

With a non recursive, depth-first traversal the stack *is* the path to the current ndoe.

Initially though, I decided to go with this API:

```cs
public static T[] Traverse<T>(
    T root,
    Action<T> visit,
    Func<T, T[]> graph,
    Func<T, bool> isGoal = null
);
```

With this implementation there was a few surprises.

(1) I feel compelled to wrap nodes. I started with T = object but ended with T = Field (custom class).
If I don't wrap, I don't get field names. Plus I'll need the actual C# fields anyways, along with the parent object, so I can capture changes.
For now I put this down as a loss. Wrapping means allocating, also defining additional types rarely feels speedy although... maybe put it down to feel.

(2) I had hangs because C#'s GetFields will include static fields, which then causes cycles. I *should be* shielded against cycling but, wrapping requires care with regard to tracking visited nodes.

(3) Getting fields isn't enough to correctly edit most Unity engine objects. Properties are needed; deprecated properties raise errors... ...overall if an automation is going to be used here, that's going to be at best a semi-automation.
