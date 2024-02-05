# Auto functions and prospective programming

## Rationale

The rationale behind planning is simple: if we can describe the output of a function as a goal, and provide atomic actions which when correctly combined will achieve the goal, we need not describe the steps needed to reach the goal.

## Auto functions

Taking a simple example, suppose we specify the following function:

```cs
auto uint Count(string arg);
```

In this case the "auto" keyword specifies that the function is auto-implemented. An auto-function resolves as follows:

(1) The parameters are used to search. Therefore in this case the search starts from "arg".
(2) We iteratively (best-first-ish) expand the parameters through introspecting their actions, and applying them.
(3) The collected outputs are verified against the specified type constraint (uint)

When running the above against "Hello", the output is 5. That is because, from a "string", the nearest positive int is obtained via "length".

In *practice*, raw auto-functions should not be running outside of a container. The premise of auto-functions is that we can try everything, and trying everything until we get the correct answer will break nothing. Obviously not all reflected, unvetted function calls fall into this category.

Additionally, in most cases we still want to define an explicit goal function, because the type constraint is not enough.

Still, auto functions are a step up from procedural programming. We no longer tell programs what to do, we just give them the tools, and let them "prospect" until we get the outputs we want.

In raw form, auto functions are not safe, and require care; still, given retrospective optimization (see 9), the appeal is undeniable - after all, the best work is no work.
