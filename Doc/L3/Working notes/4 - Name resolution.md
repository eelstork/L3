# Name resolution

It just feels like I have trouble correctly enforcing name resolution.

In the context of a running function, name resolution operates as follows:

(1) Names defined "here" (inside the current block) have priority.
(2) Otherwise look up the name in the current object
(3) Otherwise look up the name in the current namespace *as defined by the declaring unit*

Essentially what this demonstrates is: using clauses in C# provide context for short names. The implied "name space" is only convenience. For this reason let's call this a "workspace"

There is a consequence when interpreting functions and blocks: when we "enter" another unit we have to dump the current workspace, and re-evaluate (or recall) the unit's matching namespace. This is nothing like an include strategy.

An include strategy can work. However it does result in more crowded namespaces. And perhaps that's okay. Even so, I am still missing the object scope for now.
