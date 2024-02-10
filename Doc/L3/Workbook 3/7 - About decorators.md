# About decorators

Roughly speaking, L2 has only two decorator types.

The ISS decorators are used to populate key task parameters. They are, relatively speaking, variable-like:

T enemy = Folk(dragon, (pconstraints), mode, retention )

An ISS decorator can be declared up-tree, and used down-tree.

At first sight it would seem like ISS decorators are just variable definitions fetching from the apperception system. However that's not it. ISS decorators exist "in tree, out of flow" relative to the BTs.
Therefore what's deployed is a mix of usability and performance optimization.
(1) Usability: more articulate than a blackboard, they use a pull, client driven model; whereas blackboards tend to use a push model. This strategy helps making branches portable.
(2) Performance and apperception logic - following their own schedule decorators provide a rough but effective model for short/working memory. Effectively in terms of storage life cycles are tuned to simulate attention.

L2 decorative parameters essentially exist *outside control flow*. In L2 they are wired references, however this is not the key mechanism. What's happening is these decorators can be safely moved around because they are not directly code-bound. As result we have a parameter attached to a branch, but if we want to use it in a function call, in L3 we would then refer it by name.

In L2 decorators follow their own update schedule.

In summary, the plan for L3:
- decorators are attached to branches, and have specific scheduling condtions
- in general any composite should be fine. However we can limit to variable declarations and conditionals
- if a decorator returns a control value (such as a status or bool) this will apply to the parent branch
