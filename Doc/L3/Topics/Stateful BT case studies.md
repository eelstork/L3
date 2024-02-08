# Stateful BT case studies

## Doing routine work while handling emergencies

In this case we have a sequence of actions which describe "life" or "routine work". In essence although we may find advantage to defining the task as a hierarchy of tasks and subtasks, it is a linear or quasi-linear structure, which often could also be described as a state machine.

These cases also match the "assembly line metaphor" and "patrolling" activities.

About this:

(1) Whenever possible, verifying outcomes is good practice, because it allows detecting failures early, and fixing issues quickly. I think we've all seen the cartoons where assembly line glitches "snowball" while distracted workers carry tasks without a care for what they're handling.

(2) The approach to "what about emergencies" is actually simple: do a counter evaluation starting from the top of the BT but "leave the nodes ticked". So, if we have an ordered sequence, the idea is that the sequence does NOT clear nodes when interrupted. This describes tasks which can be resumed. In some cases we still need to attach an "expiry date" to ongoing work, because some things don't wait - I can resume cooking after 5 minutes, but if I wait 5 hours and the chicken stayed on the counter, starting over is wiser.

## Handling delayed notifications

With some tasks we get a delayed notification, and/or the task state *organically* is delivered as a notification.
The answer to these situations holds in two points.

(1) Wrap the task. We need a node which knows to register for the notification, and handle its done state accordingly.
(2) Decide a "while we wait" strategy. We're going to emit an assumption, which depending on how long we wait, may be changing over time. Example - ordering food:

(one minute after ordering) I'll do something else while I wait (fail)
(ten minutes after ordering) Should be arriving soon let's wait (cont)
(thirty minutes) something seems wrong, better call the shop

## Handling upstream changes

In the case of upstream changes, the situation is very much the same as with the delayed notifications...

- Leave the nodes ticked.
- re-evaluate the BT

NOTE: keep models up to date. When goods have been ordered and sent, this is not control state, this is stuff that needs to be "in the books". A great control system has a black box and, ideally, we know to leverage this to explain quirky situations. However black box or not, the BT is not your accountant / warehouse manager.
