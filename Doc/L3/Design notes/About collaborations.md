# About collaborations

When dealing with agents behavior trees lack support for seamless inter-agent communication. In general the result is that modeling collaborations is cumbersome.

A very simple example is when two agents are trading or bartering (transaction). This type of interaction will go something like:

(1) A enquire about the price of an item
(2) B confirm the item price.
(3) A confirm the deal
(4) A will hand out the required amount or more.
(5) B will confirm the amount received
(6) B will hand out the difference
(7) A will take the item

Without support on the language side, modeling collaborations quickly becomes overkill - that is, we need a model capable of abstracting high level interaction scripts from the details of inter-agent communication.

Now, let's illustrate how we'd like to implement this:

```cs
public status Purchase(Item x, ShopKeeper k){
    // (1, 2) enquire about the price
    var p = k.GetPrice(x);  // P1   
    if(p > PerceivedValue(x)){
        k.DenyPurchase(x);  // if too expensive give up
        return;
    }
    // (3) confirm the deal
    k.Accept(x);
    // (4, 5, 6) pay required amount and get change
    Pay(p, k);
    // (7) take the item
    Take(x);
}
```

In the above `k.GetPrice(x)` is conceptually an RPC (remote procedure call). All same-form methods are going to follow a pattern of generating and emitting a message, expecting a reply, processing the reply (if any) and finally returning the requested value.
We obviously should prefer handling the communication aspect in a separate program, which then must receive the call as an explicit message, something like `Tell(k, GetPrice, x)`. This is an aspect problem, cutting across all invocations of k.

On the receiving end (assuming we are modeling C-to-C communication), the implementation may look like this:

```cs
public status Sell(Item x, Customer j){
    var p = GetValue(x);
    j.TellPrice(p);
    if(j.deniedPurchase) return;
    Receive(p);
}
```

In A C-to-C context, however, the following is probably a much preferable implementation:

```cs
public status Transact(Customer A, ShopKeeper B, Item x){
    var interactors = (A, B);
    // (1) A enquire about the price of an item
    var p = A.GetPrice(x);
    // (2) B confirm the item price.
    if(p > A.PerceivedValue(x)){
        A.DenyPurchase(x);
        return;
    }
    // (3) A confirm the deal
    A.Accept(x);
    // (4, 5) A will hand out the required amount or more.
    // B will confirm the amount received
    var amount = A.Pay(p);
    var change = B.Receive(amount);
    // (6) B will hand out the difference
    B.Refund(diff);
    // (7) A will take the item
    A.Take(x);
}
```

Where *transactions* are considered:

(1) A separate controller manages interaction, alongside working memory. In a simplified model storage is unified (the interactors' working memories are modeled as one) however this is not a requirement.

(2) In managing interaction, the controller directly calls into machine agents. Conceptually such calls are RPCs, and they are made to wait (such as through the `cont` state) until the actual tasks are performed by the agents.

(3) Regardless of whether stateless or statefull, the flow and timeliness of actions is driven by the transaction.

(4) Agents support transactions through matching roles.
(see below).

(5) Communication may be handled as an aspect with respect to functions such as `GetPrice`, `DenyPurchase` or `Accept`. As an example, `GetPrice` may translate to SendMessage("GetPrice", dest), while the actual communication process (rendering human language, or some UI) is handled through a dedicated service.

A *role* (such as buyer or seller) is a composite which control may or may not traverse. Essentially the role (1) will be traversed when the agent are willing to engage into the matching transaction and (2) may declare the methods required to partake the transaction while (3) unlike a regular function, which traverses pre-determined code, a role invocation processes incoming calls.

Advantages of the transaction model

(1) The model correctly articulates interaction flows, including sequenced interactions and co-actions.
(2) Agents are not tightly bound to the transaction controller; put simply, they just process transactional callbacks in their own good time.
(3) One agent may be involved in several transactions simultaneously, including several instances of the same transaction type.
(4) Implementation details of how transactional callbacks are resolved remain local to the agent. This allows mixing  local-to-local, c2c communication with local-to-remote and hci.
