# Functions

When declaring a function the matching node is added to the scope. As a declaration the node itself is referenced in scope. This is different from assignment, which creates a runtime object (Variable) and adds it to the scope.

When calling a function we retrieve and evaluate the root expression. However we cannot evaluate this expression from the stack. Instead we have to push a new stack.

Therefore we redefine the environment as follows:

```
Stack<Stack<Scope>> store
```

The naming is... a work in progress. But for now

- everything that is stored, let's call it 'store'
- a single stack of scopes, this will be referred as a 'frame'
