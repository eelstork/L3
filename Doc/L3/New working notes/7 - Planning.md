# Planning

In "prospective programming" I looked at auto functions, which provide a means to bypass implementing functions altogether.

With auto-functions, we need not assume that the function returns immediately. In some cases we can run the solver in a separate thread. In other cases we can stagger the search. This is because, all told, we can raise a `cont` token.

However there are cases where running one of the underlying functions will involve leveraging significant resources - such as having a human operator or simulated agent carry out
an action.

In this case, instead of just "trying things out" we prefer generating a plan of action, before "pulling" and actually running the next action.

Taking a simple example, suppose we'd like to make a hole. We can purchase 3 distinct pieces of machinery, with each device being suitable for hole-making. Each piece of machinery has a cost. But we only have a limited budget:

```
auto Hole MakeHole(Person actor) where cost < 10;
```

Now, as part of this model, we have a Purchase(x) function; purchase(x) is linked to a credit card, and will place an order for us. The point being, we cannot "just call" the purchase function. In this case, instead of calling "Purchase" we're going to call a substitute.

Therefore in a first approximation, assume:

```
Purchase(x) {
    // spending real time and money
}virtual{
    // for planning purposes only
}
```

In this model "virtual" has the same signature as `Purchase` and returns an object of same type, with the difference that the return value is marked as a virtual object.

Therefore when running an auto function, if a substitute is encountered, we run the substitute instead of the actual function, and we also track costs as part of the search. "virtual" in this case has a viral element. For example passing a virtual argument to a function yields a virtual result - even if the called function itself is not a substitute.

Eventually, the primary result of running the solver is a virtual path starting from path node X:

```
[0, 1, 2, 3', 4', 5']
```

We are then in a position to run, starting from the first virtual object: replacing 3' with a virtual action.

## POC

(1) Assume an agent wanting to have a meal at a given price, and within a given deadline.
(2) Demonstrate using an auto-function to resolve the problem and implement the plan.

## Summary

(1) Auto functions prioritize virtual vs actual execution.
(2) Substitutes provide virtual counterparts to "actual" objects and functions.
(3) Auto function execution can engage a planning process.
(4) After resolving planning, the auto-function will trigger actual actions according to a formulated plan.

## Notes

(1) About goal conditions

In general, a simple way to implement the goal condition is through replacing the body of the auto function with the goal condition. In this case "out" is assumed to be representing a candidate solution.

(2) About currencies

Although I have annotated the example with "cost < 10", currencies other than time may or may not be represented using dedicated semantics.

If we are taking money as an example, the "breaking point" between experimenting and projecting happens when we gain access to the "wallet" object. A simple option, then, consists in passing a virtual wallet as argument, with a limited budget. It is easy to see how virtual supplies then limit the search.

(3) About projections and world model updates

A strips-like model is assumed. Put simply, virtual functions are expected to update their arguments; therefore assumptions about changes to the world model are for the virtual functions to handle. This approach, while provisional, allows partial updates and partial model clones.












.
