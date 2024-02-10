# Brewing coffee

## A simple design

Initially, our BT is like this:

```
Brew():
1    Add(water, kettle)
2    && Boil(kettle)
3    && Add(coffee, pot)
4    && AddFrom(kettle, pot)
5    && Wait(60)
6    && Pour(pot, cup)
```

## The stateful model

TLDR, it's going to work out. The objections are not going to crop up, or rather, it depends on the *context*.

The above sequence is mostly perfect if you just want to 'animate' a non player character in a video game. If, on the other end, you are programming a barista robot, this is not a good model.

The reason it's not is because the model captures no understanding of what each task is trying to accomplish. And the easiest way to understand this side of things, is to run this sequence statelessly.

## The stateless model

If we run statelessly, step 1 is running first. Since stateless, after completing step 1 we restart at... step 1. So we need a justification for NOT reiterating the step. The most simple justification is "because the kettle is full". In other words AddWater will fail if the kettle is already full (of water).

This allows progressing to step 2. In this case the obvious reason to NOT reiterate step 2 is because the water is hot. Therefore the purpose of boiling is to get hot water.

After completing step 2 we add coffee to the pot. This goes without problem. Let's confirm. The kettle is full, skip 1; the water is hot, skip 2, pour coffee (powder) in the pot.

Next we go to step 4. At the end of step 4, step 1 will reiterate, because the kettle is now *empty*. However the pot is full. So this is our simple "excuse" to not reiterate; at this point we need to modify the BT (whereas in all other cases, the added condition is neatly onboarded through each task implementation):

```
Brew():
    have cup of coffe => done
    Pot has no water?
1       AddWater(kettle)
2       && Boil(kettle)
3       && AddCoffee(pot)
4       && AddWater(kettle, pot)
    Otherwise:
5       Wait(60)
6       && Pour(kettle, cup)
```

This does take us past 4, and we now want to *wait* for a minute. In this case a memory node is probably the best option. Yep, a timer. Timers have state, right? In this case the memory node gets us past step 5, and finally we are done although, we risk reiterating from step 1 (if the kettle is now empty!) therefore we might be inspired to add a precondition at the top.

In passing, note how using an *object* can avoid resorting to memory nodes. In the above case we might use an actual timer object. There are benefits, and I will talk about this later [TODO].

## When does the stateful model break down?

I did mention how the stateful model is not good enough for brewing real coffee. The reason by now should be pretty obvious. To boot if we don't check the kettle for water, it's going to overflow. Now that we've fixed our task implementations (for the sake of the stateless model to even work), we can use them in the stateful model, and it's going to be *less* broken.

Lessons from this:

(1) stateless model break faster, and "force" additional modeling.
(2) the additional modeling invariably involves *data* modeling.
(3) in order to make the stateless model palatable, data modeling should be cheap.

Therefore, in one case we dodge simple errors through control state. The control state is handy, yet by nature generic and undetailed. As soon as we run statelessly, we are compelled to refine our data model, because we need to answer, over and over a simple question, what does it mean for the task to be "done".

In fact, the stateless model has only one answer in every case: "because it is *already* done". Already since when? The beginning of time? Well, a good default is "since the parent task has started". Even so, this answer does not fix the fact that stateful models are, by nature, fairly weak. There is almost invariably a better (less buggy) way to qualify what "already done" actually means.

## What is `cont`; what are we waiting for? Also, how to wait?

Now let's say we wanted to make the above model more efficient. In fact much of making coffee is made of waiting; how would an agent wait?

The good news: in a stateless context, the agent can complete higher priority tasks while brewing; this happens organically.

The bad news: depending on how we wait, lower priority tasks may not get done.

Let's modify the BT:

```
Brew():
    have cup of coffe => done
    Pot has no water?
1       AddWater(kettle)
2       && Boil(kettle) % AddCoffee(pot)
3       && AddWater(kettle, pot)
    Otherwise:
4       Wait(60)
5       && Pour(kettle, cup)
```

In this case % indicates near simultaneousness. It means that we can progress past step 2 once both tasks have been completed. The agent aren't multitasking. Rather, we can do this because "boiling" is not a task which requires constant attention. Technically this means that we do not need the agent to actuate the water temperature - the kettle should have its own timer/actuator.

Therefore, if the button is pressed, "boil" returns cont, and this frees our agent for doing something else... perhaps.

Unfortunately we cannot use this trick for waiting in step 4, here's why:
- Wait will return cont.
- A caller running Brew() *cannot* interpret cont as meaning the agent being available. That's because, overall, Brew() returns cont at any point, until the coffee is made.

Can we return a different state? In classic BT it's either cont, fail or done.

We cannot return done, because the coffee is not ready. If the next step is drinking the coffee, that's clearly going to pose a problem.

We also cannot return fail, because it's introducing a lie factor. If the fallback to make coffee is "make tea", clearly that is also going to pose a problem.

In classic BT cont is a logical state. It doesn't convey any special meaning beyond resolving sequences and selectors. As such it captures the notion that something is "in progress", thereby holding us from progressing to the next action (be it a fallback, or successor node).

In the standard (planning) model *cont* signifies emitting an action. This interpretation is beneficial. For one, it prevents BTs from generating ambiguous responses. If we adopt this position, we can also use BTs through languages which do not support a shorting ternary logic. However the "trick" used in step 2 is not going to work anymore.

There is actually a *simple* distinction to be made here between.

- "cont" => waiting on the agent's own action.
- "cont" => waiting for something to happen, without the need for idling.

Therefore, if for whatever reason sticking around is essential, generate "idle", whereas, if we are waiting for something to happen, we use fourth state, noop (no-op!).

The question then becomes, how do we handle no-op. Can this state be handled *gracefully*, or not?

Let's summarize the situation.

(1) We have not returned done or fail because it would be a lie.
(2) We have not returned cont, because the agent are not busy.
(3) Outside of GPLs, which tend to have limited support for multi-state logics, the question is, how will the rest of the BT react to no-op?

Specifically, our goal in emitting no-op is to signal that, even though the emitting task is in the cont state, the agent themselves are not busy.

In the context of a sequence, cont would interrupt the sequence. However letting the sequence run may not be a great choice.

In the context of a selector, the same is true. Because as we've already said, we do not wish to trigger a fallback task.

Whereas, in the context of an *activity*, no-op is valid, that is because an activity means "do what you can". Therefore it explicitly does not care about tasks getting done or not, all it cares about is honoring the cont state.

## Does a valued BT solve the problem *better*?

With a valued BT, we can reformulate the waiting problem somewhat differently. But first, what is a valued BT?

Valued BTs generalize logic and BT execution to allow tasks to return values, in lieu of returning "done". So, this is an attempt at "salvaging" return values, which BTs (and coroutines) inconveniently borrow from everything else.

Having a return value has one immediate benefit. In this case we're going to return "coffee cup" obviously.
This means that a client which then would *use* the output of Brew() can be prevented from doing so, through NOT returning an object (we'd be getting ahead of ourselves trying to expand on what this means).

How far does this take us?

- we still cannot return cont, which signifies action.
- we still cannot return fail (that would be a lie).
- yet somehow we're thinking of returning true? Because we're able to distinguish *that* from actually returning an object?

Well, no. Valued BTs do not solve this problem.
