# LLMs and automation

Summary:

(1) Until filled in, I see orchestrating flows of goods and services as what the software is helping with.
(2) In this context a planner should be a better choice than a BT, or we should be good with integrating BTs with planning solutions, which I'm testing now.
(3) The LLM can be used to orient planning when the going gets tough. That is, areas where a complexity increase is detected.
(4) Review possible uses of the LLM besides writing code. Can the LLM be a good critique of a BT we've developed? Can we use it to detect missing component tests?

## (1) Horse-before-the-cart approach

LLMs are a brand on AI focused on unlabeled data. In software engineering we have much labeled data. Of course there is success around using LLMs to write code; whether this approach can be replicated... we can experiment to figure whether we have enough data, in the meantime we have other options.

The overall suggested approach is, where we have labeled data, leverage the data using conventional techniques.

## (2) The hard problem

The hard problem is starting from a textual and not so formal description of the BT, and hoping for the BT to appear.

Okay to investigate.

## (3) The not so hard planning problem, and where the LLM comes in.

This is where we can formally describe the outcome, and the available resources, and let a classic planner fill in the steps. Until I gain a better understanding, how I view the business is "orchestrating the brokerage of goods and services". This is an ideal target for a planning system. Provided we can describe each node in terms of input/output surely we need not manually code the in-between steps. Also, this kind of system is tolerant to change. You can add/remove nodes and the graphs will update automatically.

So, here's a thing we may try, which approaches the hard problem.

(1) Have the LLM write formal validators for the goal state.
(2) Have the LLM write formal component tests
(3) Resolve the plan
(4) Feedback errors until stable

There are three key elements to this strategy.

(1) We can use human language, alongside conventional programming languages, to leverage the LLM. The kind of input which existing, at-(not-our)-great-expense pretrained LLMs can handle.
(2) The planner itself is 'logical' - it isn't going to hallucinate.
(3) Small steps with a cross validation strategy.

## (4) Helping with incremental change

Incremental change is when we have a working BT, and we'd like to add a use case. In general this is something that can be automated. The core of this approach consists in focusing on building tests, and automating the generation of BTs which pass the tests.

I just don't know whether this is applicable to the business problem or not.

## Where I see LLMs as useful and usable.

- The "mean end" of planning, where combinatorial explosion defeats "blind" planners. In this case the LLM can be used to drive a best first search, and I'm seeing similar strategies starting to appear.
- Filling gaps and doubting labels both in the money/time/value labeling of nodes and (perhaps less) in discovering hidden dependencies between actions and the world model.
