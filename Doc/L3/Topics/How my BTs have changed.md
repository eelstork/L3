# How my BTs have changed, and are changing.

(1) Increasingly, I use BTs as planners. This has several meanings, however "use as" does not imply "must use as". In general the BT itself chooses to yield. Therefore using BTs for actuation is still possible (at times, useful).

(2) Going towards a more strict interpretation of `cont`, where:
- cont is associated with producing a decision, which the agent, if available, must implement.
- cont is "inconvertible" and terminates the planning session.
- cont must not be used to signify an external process the agent are waiting for [NEW, IN TEST]

Here is why:
- using cont differently and allowing "flipping" the cont state caused concurrent decisions to be emitted. In general this situation was considered an error
- if we can agree that cont signifies action (with mono-tasking agents), then BTs can be used in any GPL, such as through *raising*

However...
- This interpretation may be insufficient/incomplete in a multi-agent/multi-resource context, where we are going to allow composing a decision as simultaneously actioning several agents.

(3) Where cont would be used to signify an external process is being waited on, `no-op` is emitted.
no-op signifies just that: an action is in progress, but the agent are not doing any, and they remain available for doing other stuff.
no-op has correct resolutions with typical composites, therefore this does not lead to switches.
Sequences and selectors yield when encountering a no-op, this is similar to cont. We don't move to the next node because the action has not succeeded or failed.
One simple way to implicitly emit no-op (which avoids having to think about which of 4 states a leaf task is in) is that no-op arises when crossing process boundaries. That is, if process B tells me "cont", I hear "no-op".
[NEW, IN TEST]

(4) I use an activity composite. An activity does not stop iterating on either fail, done, or no-op.
Activities are "do what you can" composites. They allow assigning a bunch of tasks to an agent, and they will go with the first task which "keeps them busy". Therefore they help maximizing resource usage. Obviously, this works well with no-op (not new, but have not used with no-op YET).

(5) I use cooldowns A LOT. Not expecting to see them as much in business, however that's a stateful strategy here which is considered relatively harmless.

(6) Refined advice and recommendations around memory nodes and stateful methods in general, including managing status notifications to help with delayed outcomes.

(7) Introducing collaborations to orchestrate multi-agent interactions (NEW)

(8) Guard conditions are better managed through content first presentation and decorators (worth explaining uh)

(9) Temporal clauses often (not always) provide an objective alternative to memory nodes.
