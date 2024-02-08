# Testing stateful behavior trees

## How stateful BTs increase testing complexity

With stateful BTs, the complexity increases because added control state multiplies the number of test cases:

- One added memory node doubles the number of cases.
- One added ordered sequence multiplies the number of cases by the number of nodes in the sequence.
- If the BT is "fully stateful" then the number of cases is multiplied by the number of nodes in the BT

As a "smell" this increase in testing complexity matches the loss of transparency sometimes incurred when using stateful BTs. In that, when we look at the behavior of a stateful BT, even if the stateful nodes are clearly labeled, figuring why the BT transitions in such or such way can be difficult.

## Oh but wait, does testing complexity really increase all that much?

Fully stateful BTs are actually much more like state machines. They are inductive, in the sense that
- The current node strongly predicates the next node.
- The amount of information needed to evaluate the transition is usually very small.

Now, in theory this should mean that testing overheads do not increase that much. In practice... I can't wrap my head around this without an example.
