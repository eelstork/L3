# Collaborations

## Why

Most BTs I have seen describe interaction from one agent's perspective. This is fine if we're controlling only one agent, or the agents are not actively collaborating.

When trying to "orchestrate" agents, multi-subjective control is a pain. TLDR describing communication from a unique perspective works well, multi-subjective does not.

Therefore, assuming a SomeCollab() function, semantics are provided for:

- identifying foreign agents (the 'agent' keyword)
- identifying placeholder functions, which bind collaborations (the 'via SomeCollab' modifier)

Collaborations do not specify greedily "locking" agents. To the contrary each agent stay in control of their own BT. An agent can seamlessly engage multiple collaborations.

## What it's for, and what it's not

- Managing and smoothing interactions between agents, without limiting the number of agents, or greedily "locking" agents.

- Use collaborations either for simulation, or to mediate communication between virtual agents, including when virtual agents act as proxies for human agents (see examples, below)

- Not responsible for each agent's book-keeping. Agents are expected to keep tabs on whatever value matters to them.
To put things in perspective, a salesman is not an accountant. If a sale somehow "falls through" this is going to show in the books, and another process will take care of that.

Examples
- Player/Application controller: I use BTs for these, but we don't need collaborations because the BT represents the player.
- User to computer agent : use collaborations when user and the computer agent are separate processes (such as interacting with an in-game shopkeeper)
- User to user: use collaborations when interaction is mediated, such as going through the network, or just computerized avatars.

NOTE: RPCs not included.

## How it works

Collaborations are best understood with an example. Here let us consider a collaboration between a customer and a shopkeeper.

Let's start with the Purchase function, which looks like this:

```
void Purchase(agent customer, agent shopkeeper, string item){
    customer.Request(item)
    &&
    shopkeeper.Present(item)
    &&
    customer.Receive(item)
}
```

Now, the customer initiates and *chase* the purchase, like this:

```
Purchase(this, shopkeeper, item)
```

So, the crux is that we want a third party to perform a task; in this case the shopkeeper are expected to present the item. However we can't directly call into the shopkeeper's functions, since they are an independent agent.

Therefore we "leave a message".

On the shopkeeper end, there is a `Sell` function declared as follows:

```
void Sell() via Purchase;
```

When the shopkeeper's BT traverses, a message sent *via Purchase(...)* is picked up, and will run. Next, the message is immediately discarded.

Messages are not queued, and the collaboration is not "objectified". In essence, the party which initiated the transaction are expected to chase until the interaction is resolved.

## About concurrency

There are no "special rules" pertaining concurrent messages. As an example, "Sell" generates a channel for "Purchase" related messages. This channel has one slot. If two customers initiate a transaction at the exact same time, one of the messages will be ignored.

This is by design. A shopkeeper may be handling 3-4 customers simultaneously. The customers are chasing, and the shopkeeper may start multi-tasking.

While additional provisions may apply in the future (such as "muting" a source for a given period of time):

- Engaging in a collaboration is optional from the receiver's point of view.
- The record allows monitoring confusing/complex situations, and responding accordingly, be it upstream (before entering "Sell()") or downstream (while resolving EPCs)

## About "broken" communication

The same remarks which apply to concurrency apply to broken communication. In the shopkeeper, an obvious instance is when the customer just take the item and make a run for it. So, how would we handle this?

(1) It is the shopkeeper's responsibility to "keep tabs" and a well implemented collaboration will provide for this (that is, a hook for acknowledging payment).

(2) A running customer is a problem which exists outside (and should have priority over) the "purchase" interaction:

```
// In "Shopkeeper" BT
ChaseThief(thief)
||
Sell()
```

As such the model is flexible and articulate. It doesn't "force" a customer to chase an interaction to the end, nor does it negate remedies around incomplete purchases.

That said, continuity techniques can be used to handle these situations more gracefully.
