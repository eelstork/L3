# About model migrations

The test case for model migration is the ability system. Overall we are dealing with growable, mutable models.

Now, putting aside the finer details, which lead to substantial help with model migration, how do we hold the bottom line?

When an element in the persisted model is not recognized, data loss may occur. When loading, the element does not read in; at the next write, the element does not read out.

The current reader implementation is okay... for now. In that, when an orphan is detected, an error occurs. This is the first step.

However just failing is not going to cut it. Let's take an example.

We have an Ability object and rename the "rank" variable to "score". Upon reading the rank value is lost and an error occurs.
However at this point, with Model we're already halfway into reading the object, therefore the object is compromised... which is a lesser evil.
