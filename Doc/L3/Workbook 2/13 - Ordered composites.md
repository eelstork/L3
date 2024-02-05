# Ordered composites

With ordered composites, an index is maintained, which means that execution will proceed in order, without reiterating nodes prior to the current index.

In the case of a sequence:
- if the current node succeeds, the index advances to the next task.
- if the current node fails, the index is reset.

In the case of a selector:
- if the current node succeeds, the index is reset.
- if the current node fails, the index advances.

In the case of an activity, if the current node fails or succeeds, the index advances.

In all cases, if the index advances past the last task, the index is reset, and we go back to the first task.

## Process/site binding

When considering composites, the index indicating the current task must be stored somewhere.

Currently, indices are hashed, and associated with both the current process, and the current site.

Binding choices affect control decisions; as an example, in the current implementation, a composite downstream of a function does not care what is calling the function. This isn't necessarily a disadvantage.

Alternative bindings may be implemented:

- Stack binding relates the index to a specific stack. Optionally, this means that parameters are taken into account, so that different parameterizations use separate indexing.
- Target (object) binding is a relatively sound option, however, currently, binding to the target object would not make sense, unless a C# object (L3 objects do not exist beyond the tick)
- Binding to a custom object, ultimately, may be the best choice. That is because many tasks only make sense with relation to a key object (or combination of objects) related to the task.

## Ordered composites are not coroutines

Ordered composites are not coroutines. As a consequence declaring variables inside an ordered composite is not safe, and may cause errors.

Where composites are used, coroutines may be a better choice. Still, ordered composites have practical uses, hopefully.
