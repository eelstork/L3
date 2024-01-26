# About BTs and animation state machines

## How does a BT interact with the FSM

First, the easy answer: the same way as you would interact with the FSM from code, but mainly through emitting triggers, setting flags, and tweaking parameters.

Now the real answer.

(1) In a best case scenario the BT send a trigger to the FSM, and the FSM, usually after a delay, will trigger the animation.

(2) In a bad scenario, the trigger will respond too late, perhaps after the BT has sent another trigger, because the agent, while waiting for animations to play, changed their mind.

(3) In a worse case scenario, the FSM will not honor the trigger, and the actor will hang for a long time.

Now, regarding (2), the solution is easy: the BT should aggressively assert what the actor want to do. In other words, model triggers as mutually exclusive.

With (3), the situation is more complicated, but first let's clarify a point: simple FSMs tend to return to the idle state, which causes simple actions to become available. Complex FSMs have multiple "hubs", which tend to reflect *stances* - not just being idle but also walking, being in a fighting stance, sleeping, being idle, and so forth.

It would be easy to qualify FSMs which always do honor their triggers as "well behaved", however that is insufficient.

## Why would a state machine not honor the BT's "intent"?

### The 'bad' reason

One bad reason why the FSM is not doing what the BT wants is because it complicates the transition graph.
In general this reason applies when stances are "compatible". As an example moving from a fighting stance to the idle should be okay... most of the time. Therefore let's assume the fighting stance and the idle stance are compatible.

However, even in this case transitioning to a non fighting action from the fighting stance may look bad, and may not be what the artist/designer want. Instead we prefer getting out of stance, entering the new stance, and *then* triggering the action.

At some level the FSM which only lets you transition from fighting to idle is "enough". However to make this good we have to put in the work (this can be done in at least two ways).

### The good reason

The good reason is when what the BT wants is nonsense. Examples are not hard to find:

- Holding sword in right hand, while wanting to grab a drink in right hand (logic: slot conflict)
- Climbing up a ladder, but wanting to attack (logic: AI (it's unsafe), art (it would look bad), design (it's against the rules))

When there is a logic conflict, this brings two questions.

(1) how do we detect the conflict?
(2) how are we planning to *fix* the situation

In general, logic conflicts can be handled with some default resolution method, but this will not always give the best results.

- An agent may sheathe their sword so that they can grab an item.
- But they may also drop their sword (it's faster!)
- Jumping off a ladder can help switch to "attack"... but if we're trying to escape up top, that's no good.

Regarding (1), at a minimum a trigger which cannot be honored should *fail*. BTs are pretty good at handling failing tasks, but we still need a model telling us "you cannot do this now".

In line with (2), essentially this is asking whether a default mecanism (fallback/sequence) will suffice, or whether we want to *explicitly* query either the state machine state, or otherwise relevant state. As an example should know that they're climbing, because being "in the middle of climbing" is a strong predictor of the next action. So, knowing the state machine state is meaningful. If the problem is a slot conflict (weapon in hand) that's also part of the "agent state" however the state machine failing in this case is the last line of defense, not the first place to look.

From there, we can determine how the relationship between BTs and state machines can be improved.

(Rule 1) Extract default resolutions. This can be done statically, when it does make sense. Essentially we need to build a map which allows saying "you want to do this, but first we need to set this flag" (and by the way, that is very GOAP)

(Rule 2) Convey sufficient information to fail invalid transitions

(Rule 3) Sorry, but we need to query state machine state.

## Side ask: why do L2 BTs "wait" for the state machine?

## Side ask 2: is it really bad for the BT to do something illogical?

Argument: failing obviously is better than not even trying, because it shows.

## Overall, what is the benefit of using FSMs - why are we still using these at all?

Animation FSMs are good at modelling constraints, including design constraints (can't move away from current state cause cheating), artistic constraints (can't move away from current state cause looking bad) and logical constraints (can't switch to walking state while climbing a ladder).

Intuition: we could do this with BTs, but we'd have to grow the paradigm towards... what animation FSMs already do quite well.
Fact: FSMs do their job quite well.
