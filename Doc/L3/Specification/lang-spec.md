# L3 Language specification

L3 is a transpective, dynamic language for multi-agent systems; a general programming language (GPL), L3 emulates key cognitive functions (narrative memory, planning, decision making, creativity) (mostly) through established, deterministic models.

L3 supports procedural, functional and objective programming.

L3 unifies behavior trees, goal oriented action planning (GOAP), problem solving (BFS/DFS/BeFS) and evolutionary programming (via STGP).

There is a draft implementation. The implementation is a work in progress; you may follow this project (contribute, even!) [here].

Transpective: remembering the past, while peering into the future.

## Who should read this?

This document is a *draft* specification; for a more engaging overview, [read here].

- If your main focus is behavior trees, [start here].
- If you are interested in GOAP, [read here].
- If you think behavior trees have nothing to do with your daily programming drudge, yet would be proven wrong, [read here].

## Execution model

In L3, a session is assumed. Within the session several L3 programs are scheduled for execution. Absent resource shortages, programs run at every tick, unless a program has requested to yield for a number of frames or a specific duration, or until an external agent (often an underlying effector) pings the L3 program.

Often, L3 programs are used for planning purposes. They drive low level effectors and run at or much below 10 Hz.

By default, recording captures every operation at every frame, along with evaluation results. Records may be RLE compressed; costly re-evaluations may be avoided through memoization.

## Units and modules

Units are associated with source files; a unit declares (membership to) a namespace and dependencies through the "using" and "not using" clauses.

A unit may pose as an external object or type bridged through the runtime, either through referencing or instantiation.

Units contain procedures, functions, and type definitions.

A module specifies namespace configuration and cross-module dependencies; modules enforce architectural integrity. Configuration may involve disabling language features, such as recording or dynamic typing.

## Literals and primitives

Conventionally, literals include integers, decimals, characters, bools (*true/false*), *null* and *void*.

*done*, *fail* and *cont* represent task statuses, whereas *maybe* and *unknown* signify uncertainty.

Primitive types (extended):

```
agent, bool, char, double, float, int, long, pro, quat, trilean, status, v2, v3, v4, v2i
```

Restricted statuses (certainties):

```
action, failure, loop, pending, impending
```

Primitive types (core):

```
bool, float, int, pro, tri, status.
```

## Statuses and tasks

Statuses may be returned either in lieu of returning nothing (void), or attached as (perhaps implied) meta-properties (when a function does return a value).

Statuses express the outcomes of a function, when thinking of the function as a task.

Restricted statuses (otherwise known as certainties) limit uncertainty regarding the status of a given task. As an example, specifying "pending" indicates a timeful task which cannot fail (the task may only return `cont` or `done`).

Additional metadata may be associated with a status; as an example, a 'cont' object may have a "delay" field indicating how long the task will take, or a success probability estimate.

Whereas a status may be an object, a task is not.

## Unknowns and probabilities

Uncertainty may be expressed through probabilities or  unknowns. The 'pro' type identifies probabilities, whereas `trilean` supersets bool and comprises the 'maybe' logical value.

Explicit unknowns and probabilities avoid confusion with statuses ("90% done is not the same as 90% likely").

There is lingering consideration for expressing unknowns via nullable values (ref); however "there is nothing here" and "I don't know what is here" are distinct situations which should not be conflated.

## Variables and typing

A variable is a name representing either a locally defined variable, or an accessible field or property.

## Variable declarations

A variable declaration implies the existence of a variable with optional typing and persistence requirements.

In lieu of type, use `var` to signify implicit typing, or `dynamic` to signify dynamic typing.

A variable may be declared inside an expression.

Variable modifiers:

`const` - specifies a variable which cannot be modified after creation

Provisional modifiers:

`ext` - specifies a variable attached to the underlying process. External variables persist between ticks and may be persisted across sessions.

`public` - specifies a variable attached to the underlying process. The host environment may read from this variable.

`mutable` - specifies a variable attached to the underlying process; the host environment may write to the variable (requires `public`).

Note: other than const, the above modifiers assume backing storage provided by the host environment. Alternatively, host side storage may be accessed through posing; a simple example of this consists in accessing a blackboard.

## Invocations (call)

A call signifies invoking a function; calls allow optionally named parameters.

The *once* modifier indicates that a call will not be invoked more than once. Without further qualification the call will be invoked once during the current session.

```
once ([after|per] [event])
```

## Operators

L3 supports common binary operators, and the ternary operator. Provisionally, binary operators may be implemented as n-ary expressions (as an example, + is implemented via the SUM n-ary operator).

The escape operators `!(exp)` and `![val](exp)` may prefix an expression `exp`; in such cases:

- if X is null, the parent function returns 'exp' or, if 'exp' is ommited the parent function emits the "fail" status.
- otherwise, the operator has no effect.

## Composite expressions

Composite expressions include blocks, selectors, sequences and activities.

Blocks define statement sequences; statements execute in order, regardless of return value, or task state.

Selectors and sequences implement behavior trees; that is, a sequence will execute until encountering failure, whereas a selector (aka "fallback operator") will execute until success is encountered.

Activities are similar to selectors and sequences; in the case of an activity, control iterates nodes until the cont state is encountered.

L3 supports ordered composites; ordered sequences, selectors and activities memorize an index pointing at the current task.

A label and description may be associated with a composite expression.

Descriptions are used for commenting; the first line in a description may not exceed N characters.
Labels may be used to dynamically replace composite expressions in a running L3 program.

## Temporal clauses

A temporal clause is used to confirm a prior outcome. The general form is:

```
did X (n times) (since Y | in the past [time]) (enter/exit/fail)
```

NOTE: temporal clauses are somewhat provisional; the main reason they are considered (instead of just accessing the execution record) is because referring past function calls without language support is ugly, and loses type safety.

## Functions

A function declaration consists in a name, optionally typed parameters and an optional via clause. The auto modifier specifies an auto function. L3 supports overloading and default parameter values. Default parameter values are not limited to primitive types.

Functions may define additional blocks;

### steady tasks

Often, a task requires working memory. In this cases we need the task to exist across frames. While this may be achieved through delegating to consolidated task objects, this approach may be tedious.

A *steady* function reiterates across frames, assuming a match among predecessors.

```
steady func(A, B, C) where COND(X, Y) until{COND}{
    init{}
    step{}
    exit{}
}
```

The *where* clause specifies a comparison between X (a predecessor, identified by its arguments) and Y (the incoming call). This clause allows deciding whether a new invocation of 'func', f' is a continuation of a prior invocation f.

The `init{}` clause may be used to perform explicit initialization when entering a steady function; thereafter step{} is repeatedly invoked. Finally upon moving away from a steady function, exit{} is called
Do not use step{} if both init and exit are omitted.

When init{} is not used, a variable declaration will skip after the first invocation; instead,

Finally the `until()` clause specifies a discard condition for the steady function.

NOTE: the correct approach to steady tasks consists in duly considering the alternative, and deciding whether there is a strong case for the language feature, in one form or another. Without getting into the details, the alternative is looking something like:

```
steady func(A, B, C){
    // build signature from "here" (that is, the current
    // stack).
    // match signature to proc storage, retrieve state
    // if applicable.
    // otherwise create proc-state, with correct signature.
}
```

Looking at the above, it feels like the use case is when we're doing just a perfect match, or simply ignoring specific arguments, while the "where" clause may be pedantic. And yet... `steady` may be what we need to clearly signal that a function uses working memory... in which case the stack object may be denied, unless steady is used.

While handing the stack does not feel wholesome, more investigation is needed to conclusively decide whether checking upstream is an anti-pattern (conventional view) or a key feature to help solving actual problems.

### Collaborations

When a function is declared using a 'via' clause the body of the function must be omitted and the function then provides an attachment point for async procedure calls (APCs); example:

```
// in Shopkeeper
Sleep() || Sell();
void Sell() via Purchase;

float RequestPrice(string item){ ... }

// Elsewhere...
void Purchase(agent shopkeeper){
    var p = shopkeeper.RequestPrice();
}
```

In this case `agent` refers an external process (relative to the process invoking the Purchase() function). Upon calling RequestPrice(), either of the following outcomes will occur:

- If Sell() is being traversed, RequestPrice() is called and the expected value is returned.
- Otherwise, RequestPrice() will return a failing status.

In practice a successful APC usually requires at least two ticks:
(1) `t[ i ]` - RequestPrice() is posted as a message binding the `Purchase` channel. If Sell() was traversed at the prior tick, the message is set (not queued); at this stage RequestPrice() returns `cont`.
(2) `t[ i | i' ]` - RequestPrice() runs "in context" (as part of the shopkeeper process).
(3) `t[ i' ]` - Upon reiterating RequestPrice(), the return value is fetched and returned to the caller (inside the customer process)

Collaborations may use RPCs on the back end side, however they should not be confused with RPC. Collaborations are designed to orchestrate commmunication between responsive agents who may be task switching, and may unexpectedly disengage communication.

As a key benefit, collaborations allow modeling non binding multi-agent interactions 'in third person'.

### Auto functions and planning

The auto modifier specifies an auto-function.

The outcome of an autofunction is resolved dynamically through search (problem solving); a search will iterate and invoke public members, using available parameters as inputs and/or combining parameters using friend operators.

Absent other constraints, the search returns when an object matching the specified return type is discovered.

The "progressive" keyword may be used to balance-load auto-function execution. Then, if a result is not discovered right away, the function may take several frames to return (in the meantime, 'cont' is returned).

When an autofunction specifies a body, the body of the autofunction predicates the goal and the "out" keyword identifies candidate output.

Example: finding the length of a string:

```
// returns a hashcode... probably
auto int Count(string arg);

// returns the length of the string... probably
auto int Count(string arg) => out > 0;
```

With auto functions, model entities precede actual counterparts; for instance, a function may define a predictive 'mod' section.

Through model objects, an auto function evaluates a plan and discovers a model solution. After the planning phase has completed, the auto-function will extract the path and start executing the plan; whereas actual evaluations do not reiterate, actual evaluations then replace model evaluations.

Auto-functions are non special with respect to task evaluation; in particular, timely evaluations yielding `cont` will interrupt evaluation. Then, upon restoring control to the process, the auto-function will re-evaluate, and replanning will occur. Memoization and/or aliasing may be used to avoid replanning.

When using auto functions, it is assumed evaluation must use all provided parameters; the "opt" keyword specifies optional parameters (arguments which auto-function resolution may ignore)

Where planning is involved, auto functions generate additional notifications and intercepts (see "notifications").

[Provisional] Where 'traverse' is used, an auto function will traverse the implied graph (depth first); via `enter` and `exit` blocks, autofunctions implement the visitor pattern.

## Classes, instances and interfaces

In L3, a class is defined as having fields, properties, constructors and methods. An instance is an object, which has a type. Classes may have constructors; fields may have default initializers.

Interfaces declare properties and methods; an interface may also define methods, through leveraging the defined properties.

## Decorators

Decorators apply to expressions (including function calls), either as prefix or postfix operations.

In general, decorators are presented as function calls; a prefix decorator may use (thereby forcing resolution of) the parameters to the client function it is associated with, whereas a postfix decorator may use parameters to the client function, as well as the return value (via the 'out' keyword).

A guarding decorator will cancel the associate expression. In this case intercepts do not run, and the associate expression is ignored (the cancellation itself may generate a notification)

A value returned by a postfix decorator replaces the output of the associate function.

## Events

L3 defines categorical (class) and instance notifications. Instance notifications are registered to objects, whereas categorical notifications bind all instances of a given type.

### Immediate notifications and intercepts

Immediate notifications signal entering/exiting a function or labeled expression:

```
on enter|exit funcname([obj], args) {}
```

### Progressive notifications

Progressive notifications signal entering/exiting a task:

```
on start [task] { ... }
on started [task] { ... }
on [task] (done|failed|interrupted) { ... }
```

Progressive notifications occur when an agent apply themselves to one same task over time.

If a task was not traversing, then traverses at the next tick, the "start/started" notifications are emitted. Whereas, if a task was traversing, then does not traverse at the next tick, the done/failed/interrupted notifications are messaged.

Progressive task events are recorded.

### Prospective notifications

Prospective notifications are emitted when an auto function generates a plan. In this case a prospective notification signals an intended course of action.

Iteratively, agents may use prospective notifications to refine individual plans; this approach may be used either in adversarial or collaborative contexts.

## Appendix A - Attributes

[memoize] - applied to a function (?or a class), indicates that the target operates on a "same input, same output" basis and is suitable for memoization.

[alias n.n] - applied to a function (? or a class) indicates that the target will tolerate a lag up to n.n time units, assuming **same-looking** input.
