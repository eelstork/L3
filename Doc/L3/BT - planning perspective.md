# BT - the planning perspective

(1) When viewed from a planning perspective, the evaluation outcome should be an action, not a task state.

(2) Other than generating the action, BT should not modify state. Therefore either the input should be const, or consist in read-only objects, or (perhaps not a good choice) disposable.

(*) When the output is an action, can we still validate reaching an intended goal?

Question: With the classic formulation, the whole BT can be viewed as working towards a goal action. But we just said that the "output" is an action. So, is there not a downside to raising outcomes

Answer: When the goal is achieved, the BT returns true, and does not raise an action.

This actually is fairly straightforward. Like other planners, once we have reached the goal, there is no work.
In this context, it is important to distinguish waiting from idling; waiting can be a strategy towards achieving an outcome, whereas idling should be a default action associated with success.

(*) Constraining outcomes

(C1) Binding outcomes to input conditions

(C2) Binding outcomes to transition graphs

(C3) Two types of transition graph

- Based on transitioning from one agent state to another
- Based on transitioning from an action to another

(*) Deliberative outcomes

(D1) Deliberating under uncertainty

Deliberating under uncertainty means considering more than just one fork, because an input needed to make a decision is lacking.
In some cases we may find that a safer alternative is available. As an example we can decide that uncertainty mandates failing the current fork, and we then move to the next fallback node.

(D2) Deliberating to resolve a doable action

In some cases we emit an action, but we then find *a fortiori* than the action is not available now. Then we can fail the action, and move to the next action.
This approach may be easier than integrating related state in the decision graph.

(D3) Can we deliberate ambiguous outcomes

How, and why, do ambiguous (concurrent actions) arise???
Then what to do???

(D4) Do "explicit alternatives" lead to HTN?

When we have an explicit alternative, it means that essentially we are saying "I think the action may be A, B, C, but I cannot choose at this point in the control graph".

So these are cases where "out of the box" resolution is interesting. We let constraints and/or projections help us figure the correct answer.

(*) Collating outcomes

In some cases we can "compose" an action provided elements do not contradict each other.
This is the case, for example, with multi-agent actions.

(*) Avoiding fallthrough

Yea... probably not much new to say about this... I think.

(*) Testing

(T1) What does testing even mean?

To work this part out, the first step is considering that, of course we can write test cases, which consist in sample input, and specifying the expected output.

With BTs however, where it becomes not so clear and needs investigating: does a Unit test make sense? In unit testing what we tend to do when doing it right is, we replace child functions with stubs. But aren't these tests entirely tautological.

I feel that, what we can do is evaluate how a change in the BT changes its response to specific input cases. So, it looks like validating "delta responses" is one way to do what unit tests are for - that is, confirming that changes in behavior produce acceptable changes in outcomes.
This needs trying out.

(T2) Evaluating long term plans and validating goals

If we consider a task or subtask; (a) the task is accomplished when returning done and (b) cont signifies performing an action.
Given the above, we can probably perform either dynamic or static steps towards validating the BT.

- In most/all cases we can *simulate*. In order to simulate we need models of how tasks modify the environment. In this respect simulating BTs is the same as extrapolating plans using GOAP.
- Without simulating we can still ask "what if this task returns fail or done? What comes next?". This allows static analysis without simulating. As an example this can be used to evaluate all "winning" strategies and, more interestingly, it allows discovering BTs that will never succeed.

(T3) Does testing BTs correctly imply having the same information as what is needed to write the stateless BT?

(*) Identifying non inputs

## About stateful BTs

[TODO WORTH AN ARTICLE]

In short, when we have a stateful BT, there are perhaps 2-3 questions to get started with the problems that this causes.
But first we'll need to ask what are these problems and what is the root cause, where do these problems come from.
Q1 - when to interrupt. But sometimes we just already know. Which is alright. And sometimes we can do contradictory (not best word), putative reevaluations.
Q2 - which nodes stay ticked, in other words where do we resume.
Q3 - what other remedies do we need to apply? How do we categorize situations to apply remedies. Categorize is a great catch word here. BTs are diagnostic processes, and here we talk about taking a different angle and again generating diagnostics.

Now, one special case that's easy to start with, is when, although we *assumed* a task was done, without verifying the outcome, we got a notification telling us "task X is undone". So we can get X undone notes, or X done notes, and this is a very simple motive to rerun the BT.

Secondly, based on this, we can design "toggle graphs". In other words we can predicate based on task status notifications. Therefore we can say something like:

X fail notice => untick T1, T2, tick T3, flip T4

So this is a simple model, and we're not sure how useful, but definitely interesting because, hey, intuitively, it's going to find some use.

With that, it's true that functions can introduce additional complexity in such cases. So maybe we should not use functions with some models, or more finely restrict their use or... obviously... put in the work.

(A) What is the appeal?

(B) What should we do when "interrupting" a stateful BT?

The primary answer is leaving done tasks "ticked" by default. I use indices but memory nodes may be much easier to manage in this (and other) cases.
I think it may be a default configuration option to say, untick when redoing, don't untick when redoing. But I'm not sure there is often a good answer.

(C) When should we interrupt a stateful BT

What needs trying, primarily, is performing concurrent evaluations at arbitrary times, which is perhaps better than second guessing interruption times... well. Perhaps we can determine interruptions statically, because we can know what states will cause a behavior change.
My expectation is that concurrent evaluations are going to cause forking, even with some memory nodes already ticked.
This approach is not going to be perfect, however I think it can inform some cases.

(D) Why we should compare stateful vs stateless execution of the same BT.

Unsure about how this is meant to work but, essentially, memory nodes imply hidden state in the input. We use memory nodes as substitutes for state modification.
I think we have more options around just ticking the node. Because we want to explicit and refine matching assumptions. So this is how we arrive at unticking these nodes automatically after a time, and so forth.

One operation i would try: if a sequence has a memory node, perhaps it should be treated as an action instead? So there may be a path to transforming the BT, which makes it more testable when memory nodes are involved.
