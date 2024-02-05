# 9 - Optimization

After reviewing a number of techniques, we can tell that some of the suggested techniques appear almost prohibitively expensive. However there is a catch...

Provided we know that the output of a function only depends on its inputs, we need not reevaluate a function which has been evaluated once in the course of a given session.

This means that we can use the record to bypass many function calls.

In one special case the input of the whole program at time T' equates the input at time T. In this case there is no work. We just return the same output we did at the previous iteration.

Now, if that sounds good, the best is yet to come: small differences in input generate small evaluation overheads. That is because the output of every function and subfunction is logged.

In conclusion, our problem here is validating the containment premise, nothing else.
