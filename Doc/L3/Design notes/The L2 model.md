# The L2 model

## In L2, behavior trees describe "director level" action

L2 behavior trees describe what happens in terms of planning steps. In an L2 tree, we see instructions like "stir the pot", "go to sleep", "pickup".

- An L2 task tends to complete within 0.5s to several seconds.
- L2 tasks are often interrupted/re-planned, because agents get involved in a thing while on their way to do something else.
- L2 tasks tend to have few parameters, but with many tweaks available. The tweaks allow the director to either override ability parameters, or help agents make sense of what they are doing through accessing the agent's memory (knowledge/working memory)

Overall L2 trees are portable. They tend to easily port from one agent to another. If we're talking about living feel / life sim they also will port from one game to another.

Currently criticism around L2 tree is not very qualified, that is to say, they feel pretty good, and allow describing action with relative ease. But, I'll make an attempt at criticism anyway.

(1) Collaborations and communication are a bit of a pain. Stuff like bartering, trading, even exchanging greetings.

(2) The process of overriding life sim elements with narrative, "scripted" action is not very fleshed out.

(3) BT composition is done through template instantiation, which does work but does not feel very flexible. The typical example (also currently, the only way composition is used) is assigning jobs. So, an agent may have a job, or not, however at this point the job is just a tree rooted into another when the scene enters. Changing job... this may be a bit of a pain. Switching a branch for another... easy, but then there are bits of knowledge associatd with a job, long story short there is *barely* or just about the minimum logic and data modeling needed to make this work. TLDR this is a good use case for *delegation*. Also this use case is meaningful. If we're looking at building lively, complex agents, they are better described as aggregating roles expressing their family, social life but also their hobbies, along with more transient endeavours.

## Abilities are "big inspector" configuration files

In L2 abilities follow a big template; creating a new ability is configuration, not coding. Abilities are resolved using behavior trees, however these trees are coded in C#

- Prerequisites (not super important)
- Navigation: where to go, but also how the agent should place themselves relative to a target.
- Animation: high level, this is mostly just telling an animation system which animation to play
- Effects: mostly manipulating the metaphor / explicit (designer owned) model but also special effects and attaching progressive (animation bound) behaviors.

One reason the big template approach is working is because agents have similarities. Usually in order to do anything they need to situate themselves in space, then either grab something or gesture in some way.

If we look at abilities with relation to tasks, essentially, like this:

Design              Technical design        Engineering
Task(2-3 params) => Ability(many params) => Implementation

Abilities, again, are portable. The description of an ability is just a bunch of parameters explaining how to perform a kind of task but, from one agent to another, the ability is (often) not going to change.

There are definite complaints around abilities.

(1) New abilities seem to pop every day. Maybe that's okay but, if that's the case having some kind of inheritance/override model would help...

(2) The "big inspector" approach combined with Unity's lack of model migration support is hurtful. Essentially the ability implementation is a swiss army knife, with pressure to evolve and articulate, but the cost/risk is causing pushback.

(3) On the engineering side the swiss army knife model is a pain. It is causing pressure because the parameter set is too large, with uncertainty around parameter combinations being actually useful, or valid. And this is providing a non-progressive starting point when trying to experiment with say, a different navigation model, procedural animation, and essentially the "cool stuff". TLDR this is one area where an expressive data model with swift migration tools is much desirable.

(4) One area where improvements are much desired is being able to replace implementation level components in the ability system. Stuff like, replacing the pathfinding or steering system, using an existing character controller... how this stuff is supposed to happen... Beyond the use case of co-evolving the ability system with some initial tech choices, not much is known.

(5) Though mostly happy with defining effects on the ability side, it feels like effects don't *actually* belong to abilities. As far as I've got, "effects" are just a separate thing, with some effects attached to abilities, some effects attached to other stuff.
While not a deal breaker so far, one way to explain this:
- Ability triggers the state machine.
- Would prefer attaching effects to virtual states (not "the one" Mecanim FSM currently working with)
- Still want some effects attached to, or at least required by, the ability.

## Queries and apperception

Before L2 how I did behavior tree parameterization was mostly through a blackboard, on the C# side.

[TODO] Somehow L2 has largely eschewed the blackboard... let's figure how this happened.
