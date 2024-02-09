# Easier block coding

Trying to figure block coding, I sense that something much more immediate is needed than pointing at a construct name, even if adopting the linear perspective is a small improvement.

How about we list callable objects matching the current context?

## The call builder

For a start what I was thinking about is, let's get callables for the current pose. However inside the graph editor we do not have a component association. This is a problem: if the script is not bound, what is the pose? To resolve this problem, I allow "as" to refer the type of a unity component. If the component matches the default pose, we do not instantiate the posed object.

Next, how do we retrieve callables? Sure, I could use reflection but, this means duplicating code. It would be better to use a genuine runtime instance, go ask...

This didn't really pan out... well not as expected anyway, did make a simple "call builder".

Reflecting this stuff and building a UI for it does take effort.

## Is it not just too much reflection?

What I'm thinking is, frankly the name associations pre-exist any related code. The dictionary approach I used in L2 would still work here. Also, it would work whether the desired function already exists or not, which can be an advantage.
