# Wood cutting using GOAP

To get us started with this let's quickly recall the "wood cutting" problem. This is a toy problem where the goal is to get firewood, and the agent have 3 actions, GetAxe, ChopLog, CollectBranches.

The actions are described in this way:

`GetAxe`
precond: !hasAxe
output: hasAxe
cost: 2

`ChopLog`
precond: `hasAxe`
output: hasFirewood
cost: 4

`CollectBranches`
precond: none
output: hasFirewood
cost 8

If this seems simple, that's because it is. GOAP was designed to help agents *quickly* evaluate plans through modeling game environments *in simple terms*.
Therefore, I tend to think of GOAP as a compromise:

- Simple, and quick to evaluate.
- Actions do not take parameters(!)
- The "world" is a bunch of strings
