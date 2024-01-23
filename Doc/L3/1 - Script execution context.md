# Script execution context

Before we can do anything useful with L3 scripts we need an execution context. Here are some candidates.

(1) The L3 script is a Unity component

That is an attractive option because it is conceptually simple. However from a point of implementation we immediately incur having to call support GetComponent<T>() in order to do almost anything. Also we need at least basic support for the . operator.

(2) The L3 script either uses or poses as, game object components

Well, assuming that's the case we'll probably want to have a number of "as" statements, so that we can at least avoid conflicts when we need to.

(3) The L3 script may call L2 abilities

Here the obvious advantage is backward compatibility with a system that's got... perhaps 4 to 6 months time investment... but also the mid-term plan is to recover most L2 functionality without inheriting errors from the legacy solution.
