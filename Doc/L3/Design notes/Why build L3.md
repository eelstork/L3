# A few reasons why L3 is useful

## Transparent execution

## Leveraging narrative memory

## Resolving "BT fall-through"

Symptom: an agent will display unexpected behavior, such as resuming a mundane task (low pri) in the middle of handling an emergency.

Solution: this may be resolved with certainties. In a related case study I demonstrate how returning a restricted status 'forces' reorganising the behavior tree.

Status: solved in C#, however certainties have not been used much because they stretch both the C# compiler and built-in logging.

## Resolving action conflicts

Often the planning result (the desired output when running the behavior tree) is a unique action, which decides what the agent will do next.

This disposition, however intuitive, flies in the face of how traditional BTs execute, since BTs return a status, not an action.

As it is, production BTs have been observed to return 0, 1 or several actions.

There are several approaches to this problem:

(1) Detect incorrect output at runtime, and raise an error.
(2) Raise the action (similar to exception raising)
(3) We are doing it wrong, the BT should *return* an action.

Status:

So far (1) has been used. However this is not very satisfying; we'd much prefer pre-empting the issue at compile time. Depending on the environment, getting at the concurrent stacks generating the actions may be difficult.

NOTE: the "one action" approach does not cover all uses of BTs. There are BTs designed for multi-tasking, which at times is making sense even when just one agent is involved.

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
