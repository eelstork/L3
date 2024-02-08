# About stateful BTs

## Where is the appeal of stateful BTs

(1) To many of us, initially stateless BTs look daunting

First, let's get one thing out of the way, as a model for managing real time, stateless BTs are actually *not*, at the outset, intuitive.

We tend to gravitate towards BT because we want programs to *yield* and are looking at situations where it feels like the answer is an action queue or coroutine.

A first experience trying to leverage stateless BTs may not be so successful. As programmers, we do not ordinarily "justify" why programs do the things we make them do. This is a problem, however it is also a strength - computer programs unquestioningly execute sequences of instructions in order.

Upside: "Do as I say, because I know it's right"
Downside: "I'm not".

The stateless BT is a computer program that will not move forward unless the current action is *currently in a done state*. Each action must verify whether the task needs doing or not.

Elsewhere, I would argue that this is a good thing, all the way to rethinking procedural programming in general.

(2) Verifying tasks may be hard, impossible or disingenuous

The key difference between a stateless task and a memory node is like this: when you have a memory node, the program perform the action then tick the task, without verifying an outcome.

Let's take some examples of when, in good faith, we should want to just mark done, not verify done.

(*) Some outcomes, are impossible, or hard to verify.

One example which comes to mind is cooking. Recipes are full of steps. A times we read "do x until the dough is looking smooth and shiny" or such. At times we're just being told to go at it. Some things are easy to check (did I put the flour and eggs in the bowl?) some things are not (is it cooked?).

In many cases we can verify an outcome, but we cannot verify in-between steps.

(*) At times validation is *delayed*

Be it throwing a javelin or ordering food, there are many scenarios where doing the task is one thing, and getting confirmation happens later, if at all.
Sometimes we can, and should wait for confirmation. At other times, moving on is the better choice.

(*) At times validation is genuinely too much work

When we are doing life loops for non player characters, we are not doing life simulation, we only do it for the feel. This is a situation where, initially having to simulate things in order to get correct outcomes... at first, it's interesting, eventually, it gets old, and at times, it may be counter-productive (in the sense of leading to undesirable behavior, not just being a waste of time)
