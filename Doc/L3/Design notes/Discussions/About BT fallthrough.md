# BT fallthrough

Usually the pattern is like this.

```
Root
    Escape
        Run || Hide
    || Doodle
    || Idle
```

In the above example, if Escape is traversed we do not expect 'Doodle' to be traversed. However this is exactly what will happen if Hide fails.

Can this be resolved using certainties?
Ref: https://github.com/active-logic/activelogic-cs/blob/master/Doc/Reference/Certainties.md

```
Root
    Escape(pending)
        Run || Hide
    || Doodle
    || Idle
```

In the updated version, we specify 'pending' as return status for "Escape". This means that escape, if traversed, cannot fail. Through type verification, the compiler will invalidate the above program, which then may be corrected as follows:

```
Root
    Escape(pending)
        Run || Hide || Cower
    || Doodle
    || Idle
```

Assuming Cower(loop) the compiler is momentarily satisfied, and unexpected behavior would avoided. However we now see that Doodle and Idle will never get called. A good compiler will also detect this condition.

The next version:

```
Root
    threat
        ? Escape(pending)
            Run || Hide || Cower
        : Doodle || Idle
```
