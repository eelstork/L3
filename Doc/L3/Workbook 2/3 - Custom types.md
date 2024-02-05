# Custom types

## Assigning an object field

Something like...

```
range.min = 3
```

This is prone to the ref/deref confusion. In this case we are not trying to evaluate range.min, instead we only wish to reference it, and we want to get a matching "Assignable".


## Access

First, evaluate the first element, make it current.
Then, for each subsequent element:
(1) if the element is not accessible, raise an error.
(2) otherwise, find the next element Y inside the current element X, which is done as follows:
(2.1) if Y is a variable, find the matching variable.
(2.2) if Y is a function call, find the matching function in X, and call it as a member of X.
