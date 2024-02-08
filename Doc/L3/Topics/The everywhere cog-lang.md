## BT everywhere

## 1. Active logic is dead

- With the standard model as requirement, BTs can be used in GPL (almost) without a library.
- This result can be leveraged provided the considered BT is a well formed planner
- Stateful constructs are still harder to implement in GPL
- Type-stronger active logic strategies... some library support needed.

My experience: BT is useful for planning, and we easily exceed the GPL in high level planning. However effectors (whatever does leaf task in the BT) "have their own flow" and they are often implemented using the GPL. So, this is where the result is most applicable.

How it works:
(1) use the conditional logical operators for control.
(2) either *raise* the output, or *store the first encountered decision* then *ignore* further emissions.

This isn't exactly new. I sidelined this approach initially (a) because I didn't start from the standard model and (b) because both approaches generate overheads which are okay for driving robots and business applications, but not okay for gaming - perhaps not even okay for low frequency planning BTs.

Where useful, I would focus effort on implementing logging (often a requirement for doing realtime right) through available aspect libraries.

## 2. L3

GPLs are getting old, and they are getting polish. When tech's getting mature, it's a clear sign that we need new tech. Looking at a language right now which uhm, seems like a GPL from the future.

My problem:

- Need headroom. Being able to use BT with the GPL, sure. Also the hill where traditional control dies on. At some point "pushing the language" and messing with aspect libraries is no longer productive.
- Want a language that works well for programmers, technical designers / domain experts and machine agents (programs which write and/or evolve program code).
- Handle situations where we edit the code in real time.
- Lightweight, model first, migration friendly data management.
- Leverage conventional AI (problem solving, predicate logic) at a much lesser cost.

Design philosophy:
- The GPL is "missing" 50 years of problem solving - in terms of machine cognition, we should be noticing a missing link between the LLM and the GPL (the net result on my end: programming feels like doing the same thing, everyday; that's when programming does not feel like plumbing; old enough to enjoy a healthy mental routine; young enough to resent my own enjoyment)
- Today we have enough memory and processing power for computer programs to remember, and extrapolate. This leads to a "transpective" language. What's remembering for? Well, the simple argument is that programs forget everything, and we keep having to remind them. What's extrapolating for? Auto-functions. Functions we do not implement ourselves. Less work, less work.

Some features:
(1) Unifies BT, GOAP and procedural programming
(2) Dedicated support for collaborations (multi-BT/agent) which has been a huge pain when modeling social interaction.
(3) Built-in support for narrative memory as a cognitive function.
(4) Fixing traversal. Traversals are pervasive in programming, and somehow it feels like there's nuts and bolts involved in that which we keep rewriting.
(5) Extensive support for automated notifications.
