# Retrospection

The "greeting" example
When giving greetings, the problem that we're trying to solve is simple, and at the same time not so simple. An agent will greet all acquaintances on an opportunity basis but, no more than, say, once a day.

In L2, greeting was handled through adding a 'didGreet' relation. Therefore
- when X greet Y, we add [greeted Y] to the agent's relational table.
- once a day (for example while sleeping) all instances of the "greeted" relation are cleared.
- arguments to greet were selected using a query, excluding agents who already have the greeted relation with X

Frankly, this is a lot of work, and the same work needs to be done in most instances of human communication, such as: saying sorry, firing warning shots, admonishing, and so forth...

```
Greet(x) once *per* day
```
