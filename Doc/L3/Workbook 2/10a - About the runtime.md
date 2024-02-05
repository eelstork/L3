# About the runtime

Overall when I talk about the R1 runtime, there are two dimensions considered, API and data modeling.

Ref: https://en.wikipedia.org/wiki/Runtime_system

Here are some goals for the runtime.

(1) Want the runtime to be small and concise; this means pushing stuff to utility libraries.
(2) Don't want workers to distinguish between "C# objects" and "L3 objects". This distinction is not productive, and I will need bridged objects which are neither.
(3) Believing core functionality including calling functions, creating objects, defining variables, to be part of the runtime.

Now, on point (3), there is at least apparent contradiction. It just feels like, if the runtime is implementing "core" functionality, then what are workers for?

To a fair extent, however, the AST is expected to be less stable than the core runtime. There is pressure for evolving the AST towards more usability. As I see it this means that the workers are doing "small jobs". As an example r1-call processes an l3 call. So, maybe the AST allows disabling a call. This isn't core functionality, it's just usability.

## API

The API breaks down in two components.

(1) External - the external API is about creating runtimes, and extracting data. Extracting data typically happens after a session has included.

(2) Internal - the interal API is for the benefit of implementing constructs. That is constructs use a well defined set of methods to interact with the runtime.

## Data modeling

From a data point of view, the runtime environment stores the following information:

(1) The current script
(2) Working memory
(3) The record and history

## Essentials

Working memory is the most traditional component of this, and is represented by `Env`. Currently `Env` consists of:

- a stack of stack of maps ('ssm')
- an object (a C# object)
- a 'pose' (which is part of `Process` and, very likely, misplaced).

This implementation does look flawed in a few ways; meanwhile, organizing working memory involves two key factors:

(F1) Managing function calls - when entering a function, the current local "scope" is overriden. The new local scope consists in the target object (if any) and the arguments.

(F2) Managing blocks - when exiting a block, variables created inside the block are released. Whereas variables prevoiusly created in local scope remain visible inside the block.

Considering the above factors,
- the key operations affecting affecting the environment include entering/exiting functions and entering/exiting blocks.
- a scope can be implemented as a stack of maps, with each stack element being associated with a block.

Additional names are visible when running a function. Notably, importing namespaces may cause functions, types and perhaps global variables to become visible.

## Global scope

When entering an L3 program, we are inside "program scope". This is not a global scope. By comparison, if we start a python session and enter some code, we're inside the global scope (of the python session).

For now I'll stick to the following assumptions
- L3 does not have a global scope.
- When integrating with a game engine, the L3 session starts and ends with the agent script. Therefore if we have 12 agents, we have 12 sessions. The L3 runtimes do not directly share memory.

## Objects are 'virtual'

Inside the runtime, objects are considered virtual; this allows wrapping specific objects in a way that makes them easier to access. As an example, a unity scene consists in named objects. In many situations "pretending" that these objects are properties of a scene object can be rather helpful.
