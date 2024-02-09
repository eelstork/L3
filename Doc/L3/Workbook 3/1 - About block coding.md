# About block coding

Since I started work on L3 block coding has been an annoyance. The same was true for L2, however this was mitigated through drop downs, with the help of a dictionary.

In L3 typing will at least partially substitute to the dictionary. However this has not been implemented.

## The input problem

L2 relies on first / third person idioms. This means that L2 has fixed sentence structures, with (syntactically) properties and decorators as backup. L3 is language, which means a much more flexible syntax.

Flexible syntax is driving top-down, left to right (TDLR) input, which is very counter-intuitive. Humans want left to right (LtR) input or, simplified, a subject-verb-object (SVO) form.

The options I see are like this.

(1) I extend the AST to support SVO and LtR forms (idioms / idiomatic AST)
(2) I provide accelerators (editor actions) to input SVO, LtR, without modifying the AST.
(3) Change the input order

### Idioms?

If I take a simple example, let's say we want to write "customer.Request(item, shopkeeper)" in this case, this can be viewed as a tree, or it can be viewed linearly. Viewed linearly, of course there is a simple SVOC (subject verb object complement) form. Is that what is wanted here? Does it lead to a distinct AST and, is this AST viable?

The AST would look like this:

statement{
    var: customer
    call: Request
    add param: item
    add param: shopkeeper
}

### Changing the input order

Now if I start from a sequence, and I want to add a statement. Instead of picking "composite" I pick "var". And this clearly signals what I mean: from "var" there is no editor action, because var is the terminal, but I *think* I can make it work

## The folding problem

First let's add an option to fold/unfold nodes
