# L3 Language specification

L3 is designed as a transpective, dynamic language for multi-agent systems. The main purpose of L3 is to facilitate modeling agent behavior through established methods.

L3 programs integrate an execution record; the execution record can be accessed dynamically, and realizes narrative memory.

L3 is a GPL (general purpose language) with integrated support for goal oriented action planning, problem solving (BFS/DFS/BeFS) and behavior trees.

L3 is designed as a dynamic, interpreted language; in the course of an L3 session, parts of an L3 program may be either modified or auto-generated.

## Units and manifests

In L3, units are associated with files a unit declare a namespace and dependencies through "using" and "not using" clauses.

A unit may be associated with a type, in this case an object of the given type may be instantiated, or bridged through the runtime.

A manifest specifies dependencies at the namespace level. Where a manifest is available, using clauses at the unit level are forbidden. Manifests enforce architectural constraints between packages.

## Literals and primitives

Conventionally, literals include ints, decimals, characters, bools (true/false), null and void.

Additionally, done, fail and cont represent task statuses, whereas "maybe" signifies uncertainty.

Primitive types include bool, char, double, float, int, long, tri (for trilean), status, v2, v3, v3, v2i, quat (for quaternions) and pro (for 'probability').

A restricted implementation will at least include the following primitives: bool, float, int, pro, tri, status.

## Statuses and status properties

Statuses may be returned either in lieu of returning nothing (void), or attached as metadata (when a function does return a value).

Statuses specify the outcomes of a function, when thinking of the function as a task. If you are not familiar with statuses and active logic, read REF.

[TODO LIST outcomes]

Additional metadata may be associated with a status. As an example, a 'cont' object may have a "delay" field indicating how long the task will take, or a success probability estimate.

## Uncertainty

Uncertainty may be expressed through probabilities, or using unknowns. The 'pro' type identifies a probability, whereas 'tri' (for trilean) supersets bool and comprises the 'maybe' (unknown) value.

A key motivation for typing uncertainties is avoiding confusion between statuses and uncertainty ("90% done is not the same as 90% likely").

## Variables and typing

A variable is a name representing either a locally defined variable, or an accessible field or property.

## Fields and variable declarations

A variable declaration implies the existence of a variable with optional typing and persistence requirements.

A variable may be declared inside a composite expression.

The "session" modifier specifies persistence over the course of an L3 session, whereas the "persistent" modifier specifies long term persistence.

L3 sessions make sense in the context of multi-agent systems. In a multi-agent system, each agent run their own L3 program, perhaps atop another program running in the host language. Session variables are expected to be stored in memory (RAM), and associated with the agent.

In contrast, long term persistence is purportedly associated with file system storage.

Special rules may apply to both session and persistent variables; L3 storage is not intended for serializing cyclic data structures.

## Functions

A function declaration consists in a name, optionally typed parameters and an optional via clause. The auto modifier specifies an auto function. L3 supports overloading and default parameter values. Default parameter values are not limited to primitive types.

### Collaborations

Where 'via' is used, the function is not directly implemented; instead, it provides an attachment point for async procedure calls (APCs). [DEVELOP]

### Auto functions and planning

The auto modifier specifies an auto-function.

The outcome of an autofunction is resolved dynamically through searching parameters (problem solving); this search will involve invoking public members, using available parameters as input to discovered (public) functions and/or combining parameters using friend operators.

Absent other constraint, the search returns when an object matching the specified return type is discovered.

The "progressive" keyword may be used to balance-load auto-function execution. Then, if a result is not discovered right away, the function may take several frames to return (in the meantime, 'cont' will be returned).

When the autofunction specifies a body, the body of the autofunction predicates the goal and the "out" keyword identifies a candidate

Example: finding the length of a C# string

// returns a hashcode... probably
auto int Count(string arg);

// returns the length of the string... probably
auto int Count(string arg) => out > 0;

In the context of auto functions, virtual objects are preferred over real counterparts; through virtual objects, an auto function evaluates a plan, and (initially) discovers a virtual solution. After the planning phase has completed, the auto-function will extract the path and start executing the plan.

A "log [exp]" clause may be used to prevent overly frequent replanning.

When using auto functions, a parameter may be designated as "required" through the "req" keyword. A solution which does not use all required parameters will not be considered; alternatively, not using req means that all parameters must be used.

Where planning is involved, auto functions may generate additional notifications and intercepts (see "notifications").

Where 'traverse' is used, an auto function will traverse the implied graph (depth first); additionally, via `enter` and `exit` blocks, autofunctions implement the visitor pattern.

## Invocations (call)

A call signifies invoking a subroutine; calls allow optionally named parameters.

The *once* modifier indicates that a call will not be invoked more than once within a given frame. Without further qualification the call will be invoked once during the current session.

The full syntax:

once ([after|per] [event])

## Operators

L3 supports common binary operators, and the ternary operator. Provisionally, binary operators may be implemented as n-ary expressions (as an example, + is implemented via the SUM n-ary operator).

The return operators ! and ![exp] may prefix any expression X. In such cases

- if X is null, the parent function returns 'exp' or, if 'exp' is ommited the parent function fails (the "fail" status is returned).
- otherwise, the operator has no effect.

## Composite expressions

Composite expressions include blocks, selectors, sequences and activities.

Blocks consist in statement sequences. This is similar to other languages.

Selectors and sequences implement behavior trees (see REF).

Activities are similar to selectors and sequences. In the case of an activity, control iterates nodes until the cont state is encountered.

Provisionally, L3 supports ordered composites; a description may be found here [TODO REF AL]

A label and description may be associated with a composite expression.

Descriptions are used for commenting; the first line in a description may not exceed N characters.
Depending on implementation, labels may be used to dynamically replace composite expressions in a running L3 program.

## Classes, instances and interfaces

In L3, a class is defined as having fields and methods. An instance is an object, which has a type. Classes may have constructors; fields may have default initializers.

## Temporal clauses

A temporal clause is used to confirm a prior outcome. The general form is

```
did X (n times) (since Y | in the past [time]) (enter/exit/fail)
```

In this case, X refers to a call, and may include wildcards and constraints

TODO example

## Decorators

Decorators apply to expressions (including function calls), either as prefix or postfix operations.

In general, decorators are presented as function calls; a prefix decorator may use (thereby forcing resolution of) the parameters to the client function it is associated with, whereas a postfix decorator may use parameters to the client function, as well as the return value (via the 'out' keyword).

A guarding decorator will cancel the associate expression. In this case intercepts do not run, and the associate expression is ignored (however, the cancellation itself may generate a notification)

Where a postfix decorator returns a value, the returned value replaces the output of the associate function, and must conform to the same type constraint, if any.

## Events

L3 defines categorical (class) and instance notifications. Instance notifications are registered to objects, whereas categorical notifications are registered to all instances of a given type.

### Immediate notifications and intercepts

Immediate notifications signal entering/exiting a function or labeled expression:

```
on enter|exit funcname([obj], args) {}
```

### Transpective notifications

Transpective notifications signal entering/exiting a task:

```
on start [task]{}
on started [task]{}
on [task] done|failed|interrupted
```

Transpective notifications occur when an agent apply themselves to the same task over the course of several frames.

If a task was not traversing, then traverses at the next tick, the "start/started" notifications are emitted. Whereas, if a task was traversing, then does not traverse at the next tick, the done/failed/interrupted notifications are messaged.

Transpective task events are recorded.
