# L3 Patrol script

In this example we would like an agent to move right, up, left, down. As a result they are expected to patrol along the corners of a square.

In L3, we solve this problem using an ordered sequence:

```
o-seq:
    Move(right)
    && Move(up)
    && Move(left)
    && Move(down)
```

When *done* the ordered sequence will reset its index to zero; by default the unit will keep running, and the patrol loops over; if the unit has the "once" flag, the sequence will execute once, then execution will stop since the root task is complete.

The Move implementation is written in C#:

```cs
public class Patrol {

    public v3 right => Vector3.right;
    public v3 left => Vector3.left;
    public v3 up => Vector3.up;
    public v3 down => Vector3.down;
    public string targetStr;
    v3? target;

    public status Move(v3 arg){
        if(!target.HasValue){
            target = self.position + arg;
            targetStr = "Moving to " + target.ToString();
        }
        var dir = (target.Value - self.position);
        var dist = dir.magnitude;
        dir.Normalize();
        var delta = dir * Time.deltaTime;
        if(dist > delta.magnitude){
            self.position += delta;
            return cont;
        }else{
            self.position = target.Value;
            target = null;
            return done;
        }
    }

}
```

In fairly typical fashion, in order to correctly interpret the supplied commands, we need to store state. The approach to how state is stored will affect behavior. Here we store a target position; therefore if the agent get pushed will patrolling, they will still attempt reaching the set location - which apparently contradicts the supplied instruction (because we may not be moving in the direction specified by 'arg').

In the above implementation, `Move()` only actuates. If we interpret the BT as a planner this is incorrect. A corrected implementation will allow the BT to rest, without stopping the effector:

```cs
public class Patrol : L3Component{

    // vars omitted

    public status Move(v3 arg){
        if(!target.HasValue){
            target = self.position + arg;
            targetStr = "Moving to " + target.ToString();
        }
        if(self.position == target.Value){
            target = null; return done;
        }else return cont;
    }

    override protected void Update(){
        base.Update();  // because the BT scheduler is set on Update
        if(!target.HasValue || self.position == target.Value) return;
        var dir = (target.Value - self.position);
        var dist = dir.magnitude;
        dir.Normalize();
        var delta = dir * Time.deltaTime;
        if(dist > delta.magnitude){
            self.position += delta;
        }else{
            self.position = target.Value;
        }
    }

}
```

In the reimplementation, the effector keeps running, even while the BT is resting. This is done mainly for optimization purposes.

Regardless, a more *responsive* effector could be implemented, at the above effect will complete each action, ignoring BT messages in the meantime. To implement a more responsive effector we would (a) store the original command (not just the target position) and (b) if the command did change, we would then re-assign the target, even if the set target position has not been reached.

## Why a stateless sequence is not useful here

In the patrol example, the intended motion is *relative*. There is no way to implement this behavior without 'remembering' prior steps.

NOTE: as implemented `Move()` is not suitable for stateless operation. Though a corner case, it is interesting (if perhaps disappointing) to note that switching between stateful and stateless modes can have unexpected consequences.

## Would memory nodes work better?

There's about two ways we can leverage memory nodes. Essentially we can do it by storing state on the client side, or we can store it host side (the host language). I'm interested in having the Move function be a memory node, because it can be intuitive. That is, we can distinguish "move x m to the right" from "move to the right at speed x", with one being clearly a relative statement (move to the right *relative to where you are now*), another being an immediate statement.

## Memory nodes on the host side

Okay, so let's assume the memory node is host side. In which case we're not going to use an ordered sequence. We rely on memory nodes instead, right? Also, file this under benefits, because it's much more intuitive.

Being a stateless sequence we're going to iterate all nodes up to cont, at each BT call: let's simplify the sequence a bit

```
1. right
2. up
3. left
```

Then initally we start traversing node 1. As before the host initially set a target, then keeps returning cont until the target is reached. Finally we "tick" the task. But the problem is, all the steps are described by the one same function on the host side. So we iterate again, "Move" is ticked, meaning that in fact, all the steps are conflated. In this scenario we're going to iterate one step to the right, then all calls to move are going to skip. Fail.

What we *can* do, is add an attribute to Move(), like this:

[Mm] status Move(...);

This allows the client to know that Move() is a memory node. Then upon completing Move, we're going to tick the task. But, unless we cloned the whole tree, we still need a hook to identify this node. So what we can do is apply site binding on the client side, and mark the node ticked. In this case, we have a stateless sequence, however when exiting the sequence we still need to reiterate (probably the savviest, as we might also queue memory nodes for clearing, but then we'd be doing it for naught in most cases) and untick all memory nodes.

So what if control (which reiterates from the top) navigates away from the patrol sequence, then returns to it? Well, I would argue that the most intuitive behavior in this case would be resuming the memory node - the last open node, right where we left it. Therefore:

- default 1: if the composite exits, untick the memory nodes.
- default 2: if the composite is not traversed, do nothing (leave memory nodes ticked)

## Memory nodes on the client side

## A less naive model

## Keeping responsiveness

## Can we have an API which behaves intuitively either in stateful or stateless "mode"

## Summary and conclusion

(1) Use an ordered sequence when attempting to queue actions "for sake".
(2) Decoupling effectors from the BT allows running the BT less often.
(3) Stateful designs are often more ambiguous than stateless operation.
