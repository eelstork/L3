# Functional vs non functional BTs

Functional BTs introduce additional complexity. There are three related problems.

(1) Sharing across instances - in conventional models functions are static, which means that no "memory state" can be associated with them.
(2) Cross referencing. When we have a cross referenced function inside the same object, we'd like to know (or specify) how memory associated with a site is actually implemented. Do we dedupe, or conflate the allocations?
(2) Mutation, notably with respect to learning and evolution
