# About traversal

## Depth first traversal is the way conventional programs evaluate

I asked myself if breadth first search could be implemented recursively... it cannot, at least not in a conventional programming language.

As pointed out by SO commenters, this is because recursive depth first search leverages the call stack, and the call stack iterates... depth first.

As a consequence, if the call stack is made to operate breadth first, then (likely almost the same) recursive implementation of search would turn into a breadth first search.

It seems like a language feature would allow implementing search variants through telling the call stack how to execute a function.

Bigger picture: the 'procedural' nature of programming isn't about programming at all. It's about... ...procedural programming. It's a choice that just works well for describing a class of algorithms, or put another way, the low level aspects.

We can make runtimes execute functions differently, including things like...

- pick the most promising branch
- pick the branch that resonates associatively
- pick a random statement from the branch

Some of this is already happening with decorated BTs, like when a branch that's been failing too long is suppressed.

Now, another part of this is, when I look at the call stack for a recursive search, it implies the path. From this point of view, having to extract the path again feels like some kind of drudgery that could be avoided.

What all this is pointing at is, there is not enough imagination around expanding procedural programming. Algorithms may be much less code (and perhaps less work) if we could tweak this stuff.

## The path is on the stack

When doing a recursive search, essentially the path is on the call stack. However how we get the path is, well, with logical patience of sorts.

- On the way 'to' the goal we increment a pointer, so that we keep track of depth.
- Once we have found the goal, we instantiate the path. Then is the right time because we know the exact number of elements.
- On the way 'out' we decrement the indexer and fill the path right to left.

Below is a putative pseudo C# recursive implementation of BFS.
- "Order" indicates running either breadth, depth first or perhaps something else. If we want flexibility in how we search, this is needed.
- upon finding the goal, the path is extracted from the call stack
- the result is raised and caught, not returned.

```cs
public class BFSr{

    public T[] Find<T>(
        Func<T, T[]> graph, T root, Func<T, bool> isGoal, Order order
    ){
        var visit = new Visit<T>();
        stack.Root();
        return catch f(root);
        /* where */
        order f(T x) => foreach(var y in graph(x)) if visit[y] {
            if(isGoal(y))
                raise from z in stack.frames select z.y;
            else
                f(y);
        }
    }

}
```

Unsure whether I find it a huge improvement... perhaps not. Also

## Traversal is omnipresent, defaulting to recursion may not be a great idea

At the opposite end, where I've been going is essentially, the *non* recursive implementation of search, which keeps the stack/queue explicit, is actually more flexible and powerful, because we can use the same implementation to traverse in whatever way we please, and separate the graph operations from the actual search.

However... there is convergence here. If we tweak block traversal through annotating function declarations, then we've evaded the "depth first default"
