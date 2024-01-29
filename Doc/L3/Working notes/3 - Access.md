# Assignment

## Access

To implement "access", a new composite (n-ary operator) is added.
- enumerate the Type in `Composite`
- add the prefix to the list
- implement in R1.Composite

Access evaluates left to right.

For sake of argument let's say we want to access

"range.min"

In this case the left hand resolves in scope. However instead of resolving the right hand, then applying an operation, we pass the right hand node to the resolved object via (Accessible.Access).

We have to postpone evaluating the right hand because it can't resolve in scope, instead it does resolve in the accessible scope of the assumed object.

In a way this is cumbersome; on instinct we'd prefer resolving access as follows:

(1) Evaluate the left hand => L
(2) Evaluate the right hand => R
(3) Find R in L

That's because we want to ask, "what if the right hand is an expression"?

A quick look at the access operator definition in C# does not fully resolve this, however suggestively there are only two constructs valid as right sand: (1) a variable and (2) a function call.

When presented with a function call, we do not "resolve the expression first". Instead we have to (1) resolve the function in the left hand's object scope, (2) resolve arguments "in scope" (the current scope) and (3) call the function.
