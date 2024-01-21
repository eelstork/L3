# About the interpreter

With the program considered as a graph, we iterate nodes in succession, in a manner which resembles depth first search. As such, depth-first traversal is the key process characterizing conventional evaluation.

## Record

The frame record provides a hierarchic view of the traversal process. This can be implemented as follows:

1. A frame records the traversed node and its return value. Additionally a frame contains traversed sub-nodes ('children').
2. When entering a node, a new frame is created and added to the current frame. Then the new frame becomes the current frame.
3. When exiting a node the return value is recorded, then the parent frame becomes the current frame.

## Narrative record

A narrative record expresses continuity across iterations (ticks). From this perspective, if we traverse node X at iteration n, then traverse node X at iteration n+1, then X is considered 'ongoing'. Whereas if a node does not traverse at the next frame, X is considered 'exited'.

The narrative record is helpful because it captures behavioral continuities and discontinuities *over time*.

Structurally, how is a narrative record organized? One way to model this is through local histories associated with specific nodes.

In this case, starting from node X, the history is
- continuous if X keeps traversing the same children,
- discontinuous if X starts traversing a different set of children

In this case, when a discontinuity occurs we archive the child nodeset using an episode.

Next, we look into building these records.

At any point during execution only one node is current. The next event can be either of two things.

- Enter a child node
- Exit the current node (move back to parent)

Therefore, upon entering a node.

1.1 We check the next node in the current frame. If the next node is not the same as the incoming node, we archive the current trail.
1.2 However if the next node is the same, we just enter the existing node.

Upon exiting a node
2.1 If the current frame had more nodes, archive the current frame.
2.2 Go back to the parent node and increment the index

Overall, with consideration to the narrative record, each node is holding a list of episodes. Within a single episode, the history is locally consistent - that is, behavior has not changed through the considered period, with regard to child activities.
