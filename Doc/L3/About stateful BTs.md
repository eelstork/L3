# About stateful BTs

## What is the appeal of stateful BTs?

## The simplicity vs simplicity problem

Part of the problem is:

(1) stateless BTs are simple. With stateless BTs functional programming is easy, and does not generate conundrums, understanding the BT is also easy, in the sense that there is no hidden state. Many bugs are detected early, which is also viewed as a benefit.

(2) stateful BTs are simple. With stateful execution we need not ask how we know a task is done, we just assume that doing the task has the desired effect, so we need not explicit the outcome.

Therefore sless, sfull are both simple, and we'd like to find a way to get the benefits of both, which at some level is wanting one thing and its opposite.

There is also an apparent paradox, in the sense that conventional programs execute statefully (what is the stack for!). Therefore, stateful is the norm, and is intuitive to programmers. Meanwhile, programs do not normally retain state between invocations. Therefore, stateless is the norm.

## How de we know a task has been 'performed'?

## Coroutines vs memory nodes

Compared with coroutines, stateful BTs feel underpowered. Coroutines allow programs to wait while fully preserving stack state. Whereas stateful BTs are only stateful in the sense of saving the execution point.

Which one is the better model?

## Recording vs memory nodes: how recording provides a more objective angle and, can we leverage this to redeem memory node issues

With recording, part of the appeal is simple: records do not lie. There is a big difference between saying "this task was done at time T" and "Consider it done".

[then?]

## Should we just explicit what "done" means. Can we add useful metadata to the "done" state. Will this help?

## Memory nodes do not make immediate sense with functional BTs

At this point, how functional BT models interact with memory nodes is not entirely clear. Therefore the first step to clarifying memory node issues will consist in taking functional BTs out of the equation. We can reintroduce functions later, and we probably need to, because composition approaches converge with functional formulations, and we cannot scale without composition.

## Memory nodes vs ordered composites

## Taking advantage of memory nodes, when outcomes are negligible - the breakfast example.

## Building robust designs across stateful/stateless execution

One good approach to the control state problem is designing BTs in a way which allows you to
- transition memory nodes to well formed tasks.
- run a tree in either "fully stateless" or "fully stateful" mode and compare outcomes
- investigate how switching nodes between sless and sfull affects your tests (you have tests, right?)

A consequence of that is, if we have a condition used to evaluate the done state, this should be something we can attach to the BT without modifying its structure. So this is one way that we can start with "let it be considered done when the task is complete" and later correct this as "let it be considered done if condition X is fulfilled"

## References

https://dev.epicgames.com/community/learning/tutorials/L9vK/unreal-engine-common-issues-with-behavior-trees-and-things-you-should-competely-avoid
