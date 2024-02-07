# L3 language introduction

L3 is designed in a spirit of collaboration. It is a little get-together between those who need a framework, those who block-code, those who think types are annoying, and the linting lover who believe weak typing is for the weak.

Through persistent objects, dependency injection (DI) and data modeling, L3 saves your stuff.

## Who is this language for? [HIDE SECTION]

- Designers and domain experts use L3 without much insight into the procedural aspects of the language. Through block coding, what they see is a domain specific framework.
- Technical designers in need of "behavior trees" use the procedural aspects of the language without caring about types and classes. Simple cases do not require declaring functions.
- Programmers use L3 as an object language; apologetically, I do not currently (plan to) provide an L3 parser. In linearized form L3 productions constitute well formatted program code, which does help when reading diffs.

Writing parsers is time consuming. Not writing the parser helps L3 development focus on constructs and evolve the AST. This effort is supported by an (automated) visual front end. When the AST stabilizes, I may review linearizations and write a parser, or (minimally) suggest a syntax for the L3 language.

The intended use cases are listed in the next section.

## Intended use cases

Intended use cases are listed alphabetically.

(1) Business process automations
(2) Dynamic application design (through BTs and IMGUIs)
(3) Education (block coding)
(4) Game AI
(5) Robotics
(6) Simulation

Note: a language isn't much without a standard library, and some interop. Currently I am working on a C#-hosted implementation. Depending on circumstances other platforms/host languages may be considered.

## Key features

(*) Typing choices
(*) Hot reload
(*) Behavior trees (stateful/stateless)
(*) Automated, transpective notifications
(*) Goal oriented action planning (GOAP)
(*) Traversal support (DFS, BFS, BeFS, A*, Search, Visitors)
(*) Auto functions
(*) Evolutionary programming (through STGP)
(*) Record and recall
(*) Content-first presentation (through decorators)
(*) Data modeling through persistent objects, DI, model migration
(*) Interop via posers and object models (runtime feature)
(*) Deep resolution
(*) Multi-agent collaborations

## Motivation

Since 2018, I have been working with (mostly stateless) behavior trees, as implemented in C# via active logic. My experience with behavior trees has been largely positive.

Behavior trees are useful for capturing human expertise with correctness and responsiveness; in some cases, you do not need anything (other than a couple of inspired strategies) to use behavior trees through GPLs. However behavior trees are more than just control - they partake a culture of transparency which does not exist in C#, Java or C++, where you need obscure instrumentations to get the information BT users routinely rely on.

The first motive behind L3, then, is wanting a language which, rather than barely supporting behavior trees, replaces them in a forward perspective, where the GPL is the starting point, not the finish line.

While working on behavior trees, I also experimented with STRIPS oriented planners (via xGOAP). While GOAP is not a golden bullet, it is closely related to well established search techniques (BFS, BeFS, A*). Loosely speaking solving problems through search leverages traversal, a (not so) surprisingly pervasive programming pattern.

I have committed to integrating GOAP with behavior trees, and I intend to make good on that. Also... programmers should tire of writing procedures, let alone for solving trivial problems. As a case in point, while much of current AI research focuses on unlabelled data, programs are full of labels; we need more languages with *auto functions* - less code, is better than asking smart bots to write the code for us.

Finally, L3 is designed to be approachable, and grow with one's programming ambitions. An L3 programmer may fare well enough without understanding (or caring about) types, functions and classes. Another may force strong typing. It (L3) needs to work well as a block coding language, and produce beautiful, diffable text.

## Appendix B - considered issues [TODO should be replaced in context]

*Immutability* and *sandboxing* are key requirements for some L3 features to work well.

NOTE: currently the working assumptions are (1) until dependable analysis can be provided it is the programmer's responsibility to pass safe objects to auto-functions, apply tags where relevant, and claim memoizable targets but (2) we *can* exclude whole namespaces from using certain features such as recording, memoization and auto-functions, and we can also adopt an opt-in approach for some namespaces, including when targetting host environments (not every host language). Finally (3), dependable architecture can greatly benefit from enforcing module dependencies mechanically.

(1) In order to automate memoization, we need to know that a function does not access data other than its inputs, and constants; additionally we need to know that the inputs have not changed. Shallow references do not prove this. My understanding, so far, is that this requires propagating time stamps.
In some cases, a "proof in the pudding" approach may work - that is in, context where we can afford very unlikely errors, we can memoize based on "it usually memoizes, let's memoize".

(2) With auto functions, we want to ensure that performing arbitrary actions will not damage external data. How this is done is not very clear just yet.

## Appendix C - considered interests

- PROLOG
- HTN
- Dependency injection and model migration
