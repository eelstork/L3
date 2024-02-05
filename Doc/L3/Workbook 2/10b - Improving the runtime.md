# 10b - Improving the R1 runtime

Key points:
- most workers (r1 delegates) should not distinguish between C# and L3 native objects.
- construct implementations should be self contained
- local spaces are weirdly implemented. They require adding objects of type `Node`, however Node (a) is part of the AST and (b) does not require naming. This means that we can add something, which then does not have a name, and will never be found.

## Discussion

In R1, the `Process` class defines the runtime.
If I look at the runtime implementation, the most glaring aspects are the switches in `Step` and `Ref`. These switches are not great; instead being able to add new constructs without messing with these would be nice.

This is, in part, linked with a decision to have workers implemented as static classes. However there are options to increase runner integrity without having workers as objects, and without resorting to a "big add" block

```
// static block in worker type
{
    R1.Add(Type, Step, Ref);
}
```

On the service side, constructs use the `Context` interface. This interface does have some problems.

- Ideally we would prefer to not expose `Env`, `History` and `Pose`. It seems like it would be better for context to provide a facade, instead of handing out key objects on demand.
- We do not want every other construct to distinguish between C# and L3 native objects. Instead finding stuff, and creating instances should be the job of the runtime... well, that's how I see it anyway.

## About delegates

r1 call, r1 new: use reflection, directly handle Cs objects
r1 composite: has a pretty bad pattern in that we end up with much code for handling the many n-ary operators.
r1 field: looks innocuous but generates `Variable` which in turn seems to be doing much work around name resolution
r1 var: again this is doing too much work, and handling the native/cs distinction.
