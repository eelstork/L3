# Approaching and avoiding stateful control

## 3 stateful approaches

(1) The memory node helps refining how "ticking (and unticking) the task" can be made safer.

(2) Ordered composites are... for what?

(3) Ordered BTs

(4) Coroutines

(5) Timers

## 3 approaches through data modeling

(6) Temporal clauses (non stateful)

(7) Binding memory nodes to model objects and their life cycles.

"greeting" example

(8) Lighweight modeling

## No, I would *never almost ever* think of persisting my BTs

Look, I'll check this out because there's nothing like getting proven wrong (also, you seem to be having so much fun!), however this is a good time to remind myself that I wrote "BTs should not keep our books".

What I am seeing as a smell is the risk of confusing data which is critical to tracking processes, with data which helps organizing flows. There is a very simple way to explain this: human agents have memory; they're smart, they're good at organizing stuff. But also, we write stuff down. This draws a very clear line between critical information, and memos.

On this topic, I figured a simple rule that, whenever we're transacting value, we're well off engaging data as opposed to control state.

...which obviously binds to the central "why" of why I don't let go of stateful BTs, even though I find them rather flaky: most cases I see have to do with human communication.

## Yes, we can resume stateful BTs, and we can avoid the "when do I interrupt problem"

## When providing a default answer is bad

## Is memory inside the task, or do we wrap using a memory node?

## When memory nodes need flagging
