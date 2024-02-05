# Assignment

## Assign

To implement "assign", a new composite (n-ary operator) is added.
- enumerate the Type in `Composite`
- add the prefix to the list
- implement in R1.Composite

### Assign a literal to a C# object field via a `Var` node

Now, regarding the implementation of "assign", one problem which arises is that 'Var' normally resolves to a value. In this case, however we want an assignable object.

### Assign a literal to a variable declared via `Field`

Syntactically fields and variable declarations are the same. Therefore in a first approximation I'll use a field to declare function-local storage

## Assign a dynamic variable to a C# object field

In this case we expected that the first variable will resolve in-scope. The current definition of R1.Field supports this, in that a field generates a variable, which is then added to the current scope.
