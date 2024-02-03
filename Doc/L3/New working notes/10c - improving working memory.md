# Improving working memory

In R1, the smallest unit of working memory is a scope. A scope is implemented as a list of nodes, which is a poor choice.
Likely, a scope would be better implemented as a map.

Suggested renames:

```
Scope        => Block (invisible to constructs)
Stack<Scope> => Scope
Store        => Store (unchanged)
```
