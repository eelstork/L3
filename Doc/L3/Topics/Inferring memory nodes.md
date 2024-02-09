# Inferring memory nodes

Part of the problem with memory nodes and ordered sequences is that they constitute extra modeling work on the design side. They're not even that easy to explain. In sum, file this under "technical".

Meanwhile even before we start talking about behavior trees, they are a fit to informal descriptions a designer would provide. Forget about designers, most people can tell you how to accomplish a task, and most will also be able to follow a plan, and figure how to sequence actions.

Therefore, there is intuition of sorts which can be used to "uncover" the need for either memorizing or leveraging memory.

One symptom is when tasks in a sequence tend to undo each other. Recall the brewing example:

```
Brew():
1    AddWater(kettle)
2    && Boil(kettle)
3    && AddCoffee(pot)
4    && AddWater(kettle, pot)
5    && Wait(60)
6    && Pour(kettle, cup)
```

In this case we could up to step 4 without undoing a step. However that's assuming we are able to justify moving forward through steps 1, 2, 3. 
