# Wrapped objects

Ideally we should not want to wrap resolved objects. This is not desirable for two reasons:

(1) Memory overhead (minor initially but important)
(2) Confusion - when we later consider the object, we may need to unwrap it.

## When are we wrapping, and why?

We are wrapping mainly for correctly implementing the Assign composite.
On the left hand of an assignment, we want an assignable object (LValue).

For example if we write index = 10, index does not refer the current value of 'index' instead it refers to the 'index' variable itself.

Then if we write index = z = 10...

In this case all but the last expression must refer an LValue.

Now, the question is, can we avoid wrapping?

The other half of this problem is the "access" concept, because, an expression like "range.min" results in an LValue.
