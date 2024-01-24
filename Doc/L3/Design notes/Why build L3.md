# A few reasons why L3 is useful

## Transparent execution

## Leveraging narrative memory

## Built-in event support and vetoing

In most languages events must be explicitly defined; for the most part this is pure drudgery, because events related to entering/exiting functions are easily implied.

Additionally vetoing is only possible on an opt-in basis.

## Managing continuity

When handling progressive activities, at each step a program
will either reiterate branches, or fork away to other branches.

Continuity management helps with the following situations.

(1) When entering or exiting a branch should trigger special subfunctions (starters and stoppers) and/or events
(2) When control transitions should be restricted. As an example while iterating branch X we may disallow certain calls (state transition graph)
(3) When specific branches should be suppressed under given conditions

## Collaborations and inter-agent communication

Collaborations are used to model orchestrated inter-agent processes, such as trading, bartering, playing games or having a conversation.

There are a few problems associated with this which traditional methods do not handle well.

(1) Shared understanding.
(2) Narrative/logical continuity, let alone assuming interruptions and/or prioritization.
(3) Abstraction, since collaborations drive control, not the details of how each agent partake the interaction.
(4) Communication, since what a high level program represents as a method call often resolves to verbal/non verbal communication.
(5) Parallelism and medium term persistence, since agents may simultaneously engage in many transactions, each resolving at its own schedule.

With collaborations, a separate controller is driving behavior. This means that collaborations are modeled as convened and templated. Therefore a tier function is calling into several agents, and actually driving their behavior.

While a controller is driving behavior, the emitted calls will not resolve synchronously; instead, these calls are resolve on the agent's own schedule.

As an example a customer may have a `Purchase()` method while a shopkeeper may have a `Sell()` method. While we may agree that the customer initiate a transaction through `Purchase()`, the actual transaction is resolved through some `Transact()` function defined elsewhere.
Therefore, in traversing Purchase() and Sell(), the involved parties manifest willingness to progress `Transact()`.

In effect this means that `Transact()` is going to either re-iterate (stateless) or block (stateful) while waiting for RPCs to resolve. Meanwhile `Sell()` and `Purchase()` consume pending requests and advertise matching statuses.

## Decorators and N-ary operators

## Delegation

## Interest in GOAP

## Efficient handling of traversals

## Generalizing status

## Assignment through branch names... (?)
