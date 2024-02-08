# 50 years of nonsense

In the age of copilots, we've reached the top: finally, bots are replacing us in the dauting task of writing computer programs.

This is an amazing instance of trading short term benefits for long term rewards. If anything, LLMs should read code for us (and spit out much less convoluted hints than GPT3). I thought we spent 2/3 our time reading code, seems like 90%. Come again?

As it turns, perhaps the real problems programmers should be solving are elsewhere... Because we are sitting on 50 years of iterating problem solving, and regarding old fashioned AI as something we implement and reimplement through case logic. What if we had a language allowing us to leverage these tools, instead of empowering us to rewrite them over and over.

I've been trying to design such a language. Then I came across this: the auto function.

```
auto dothing(); // implementation details left to the compiler/runtime.
```

The thing is, programs are full of labels. We've reached the point where every code you'll ever want to type has been typed already. So we shouldn't be writing this code, and we shouldn't be reading it either.

You can compare this with `auto` in C++, essentially a one-char too long admission that nobody understand how to declare C++ variables.

You can stop reading. The auto function is real. Or, rather, there's about 10 ways you can go about making this happen, none of which are wrong.

float slopeAcceleration = Mathf.Lerp(
    1f, slopeAccelerationMultiplier,
    Mathf.Abs(Mathf.Sin(slopeAngle * Mathf.Deg2Rad)
)
);

## Reminiscing and extrapolating, two things computers can do now

Because power and memory... except oops, we've been having the power and memory for at least the past 20 years, if not 30.

## THings we shouldn't bother with

- Trivial conversions, including hinted through synonymy.
- Search
- Traversal
- Loops

## So, what is the LLM for?

Turns out,

## The social and less social failures of computer programming
