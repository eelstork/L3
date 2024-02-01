# Scopes

When entering a scope, variables declared "so far" remain visible inside the scope. However when exiting a scope, variables declared inside the scope must be deallocated.

In the current implementation, a Variable is created through the Field object. r1-Field will invoke env.Def() to bind the matching variable.

In order to enforce scoping, "env" is modified as follows:

(1) When retrieving a name, all scopes are considered (iterate the stack)

(2) To Env, we add the EnterScope and ExitScope methods (for now we cannot use Push and Pop because they affect the object scope)

(3) Before evaluating a composite, we push a scope. After evaluating the composite, we pop the scope.

The above strategy resolves the following:

```
block{
    int x = 10
}
Log(x) // must err
```

Through this definition "assign and log" also fails:

```
assign{
    x : int
    10
}
Log(x)
```

This is because "assign" is itself a composite, causing x to get scoped. Obviously, assign has to be a special case - that is, an "assign" composite does not define a scope.
