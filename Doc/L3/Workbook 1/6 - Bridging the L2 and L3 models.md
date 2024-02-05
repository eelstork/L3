# Bridging L2 and L3

## L2 queries

In L2, queries are decorators used to both parameterize tasks and fence irrelevant tasks.

As an example, "hostile encounter" is a task which assumes an enemy. Therefore we define an "enemy" query.

Tasks down the tree will use "enemy" as a variable

An equivalent to the enemy query would be defining an enemy variable. In C# strictly speaking there are limitations, because defining variables mid-expression isn't really a thing.

In L3, we can probably define a variable inside an expression. Also at some level, it would actually work. However that is probably not the right answer.

The significant reason why not is that we'd have to enter "enemy encounter" then define our variable. But we don't want to traverse the branch if there is no encounter, and this is important for recording purposes if nothing else.

Therefore we use a decorator. A key advantage of the decorator here is that it's essentially out of flow with respect to the expression tree.

Other than that, decorators are calling into an apperception system and the metaphor; however this can be mostly ignored as an implementation detail...

In summary, queries were used as follows:

(1) Through decorators, which are defined "out of flow" with regard to the expression structure.
(2) Decorators query an apperception system, and the underlying variables often have a slow life cycle. The slow life cycle, when applied, is by design.
(3) For the most part we could emulate these decorators in a GPL; the obvious downside in practice (in principle not a requirement) is more parameter passing and more drudgery (because, no variables without statements).
(3) Vars through decorators lend themselves to scoping and de-scoping, which is good workflow.
(4) Vars through decorators lend themselves to narrowing, which in principle, is bad (less explicit) but in practice is palatable (elide minor details to track the big picture).

Overall: wanted in production, not critical for a proof of concept - because we can have vardefs in-tree, including redefines.
