# Testing behavior trees

## 1. Component testing

### 1.1 input/output testing

When we talk about component testing with relation to BT, this should be understood as "test the BT".

With sless BTs this can be *relatively* easy when the standard model is used.

(1) The BT uses a blackboard for input. No other data is input from the BT.
(2) The BT is a planner, responsible for emitting a decision.

In this case, we can create simple test cases, which may look something like this:

```
Input(A, B, range[0, 5]) must yield myAction(X, *, Z);
```

In summary, we verify that a given input, or input range, is associated with a given output, or output range. In the above mockup:
- range is going to traverse values from 0 to 5
- the wildcard "ignores" a given output

Insofar as BTs are concerned, the standard model is the benchmark for testability. If we're trying to figure how to test BTs, we should be comparing our BT model to the standard model, and consider the differences.

### 1.2 simulating

Another approach consists in (1) generating a test case then (2) simulating execution and (3) verifying that the BT reaches the "done" status within a specified time frame (a number of frames, or actual time)

## 2. Unit testing

With sless functional BTs, again, unit testing is relatively straightforward.

In this case, the input is simply whatever parameters we pass to the function, and the "output" consists in.
- functions which are being called downstream, and with what arguments.
- the returned status

This approach can be extended to non functional BTs. Which means that you can test every branch in this manner. However unit testing branches, let alone by hand, may turn into a dubious exercise.

The general idea here is that when you have too many too small units, the cost of maintaining them increases. Meanwhile tests become tautological, with no utility beyond "double check, double code our work".
However do note I didn't say "without utility". When double checking their work, programmers probably fall in two bags.

(1) Actually double checking that everything is fine!
(2) Losing interest and defocusing, in which case no benefit
(3) Fully automating the test, in which case no benefit (double coding without double checking), and the illusion of good work done (the tests are done!)

## 3. Coverage testing

In my past projects the lack of coverage tracking has been probably the most obvious source of hurt. Often not traversing specific branches was the obvious problem, however we didn't know the branches were not being traversed.

Coverage testing requires either QA (manually exercise the BTs) or a mix of i/o testing and simulating.

## 3. Trickier configurations; dos and dont's

Blackboards do not scale. Often this only means that the input structure is more complex. It is not a problem, however setting up testing is more work.

Do: put in extra effort to put BTs under test, even when the input or output has complex structure

Don't: normalize the input/output structure. This leads to swiss army knives, which are error prone. Tests need to "understand" that perhaps we have 3-4 types of actions as output, and the input is not a vector.

Don't: multiply input/output attachment points. In my opinion you need a very good reason to do this. If you really own the BT solution, your test suite may cope. However the problem is not here - the issue with multi-hook i/o is a loss of clarity.

## 4. Valued BTs help testing "production graphs".

Where cont tokens are used, and the main purpose of the task is producing something, functional BTs with cont tokens are testing friendly. This should be fairly straightforward. If a task produces something, then checking the output as a return value is the easiest place to look.

However:
- I have hardly scratched valued BTs.
- At first sight, it feels like valued BTs break the standard model.

## 5. Certainties reduce the need for testing.

Static analysis can tell us simple, yet useful things. For example it can identify the possible outcomes of a BT.

Static constraints are a better equivalent. Through certainties we directly specify (and implicitly, validate) the correct outcomes for a given BT or status function.
