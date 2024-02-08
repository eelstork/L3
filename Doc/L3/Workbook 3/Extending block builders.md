# Extending block builders

Okay so I have made a call builder. Here is what is missing.

- Similarly starting from properties or fields, then emitting a call.
- Chaining calls. This is pretty simple, in that once a call is fleshed out, we have an option to chain another call. Stay at the "dot" level as we do not want to do this with other composites for now.
- Nesting calls. This is an option when supplying arguments
- Dictionary alternatives. This is useful when reflection returns nothing.

## About supporting L3 objects

In short, assume that most of the code is already written. This is reasonable as I am not going to need, or want, different signatures for reflecting L3 types.

...even if some features like access control are not getting implemented very soon.
