# Retrospection

Through retrospection, a program accesses a structured record of past execution; retrospection is a simple feature, however it is rarely implemented beyond diagnostics.

Beyond maintaining a structured record, experience shows that reified access eases retrospection.

## Use cases

(1) Calling a task "once"

Retrospection allows performing a task "once" while clarifying the meaning of what "once" actually means; examples:
- "once per day"
- "once in the past week"
- "once" in the context of the current function
- "once since [cond]"

Known use cases:
- many examples in human communication, such as greeting, issuing warnings, and so forth.
- implementing cooldowns
- algorithms; as an example, "once" simplifies traversing graphs (each node is visited "once").

Retrospection captures past return values. Therefore, it also allows retrieving the results of past evaluation without resorting to explicit storage.

(2) Continuity

Through retrospection, an agent determine entering and exiting a task. In this case:
- a task is "entered" if it was not traversed at the previous frame.
- a task has "exited" if it was traversed at the previous frame, but was not traversed at the current frame.

Continuity allows "smoothing over" discrete evaluation frames; in some cases we consider continuity across ticks, in other cases we consider temporal continuity, thus ignoring short interruptions.

(3) Explanation

Retrospection allows explaining why a program is not performing (or did not perform) a given task. With respect to a single frame (by extension: a time span), we are able to extract paths leading to the task, and identify faulting forks.

Example: why did you not deliver the package?
=> was busy handling an emergency
=> ran out of gas
=> could not find the address.
