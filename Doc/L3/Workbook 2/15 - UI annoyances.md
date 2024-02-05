# UI annoyances

Frankly editing the AST by hand can be genuinely a pain, so anyway let's point out the key annoyances.

- The dot/access composites are supremely annoying.
- A lot of the time calling functions needs a target. Which then kicks the dot access annoyance
- Likewise declaring fields is okay, whereas declaring AND initializing variables is just annoying.
- Just even assigning variables is... yea... still annoying.

As I see it part of the problem here is that the AST undermines the subject-verb-object structure. It's forcing thinking input in a top-down manner. But humans don't "think the grammar" before thinking sentences. So, that's probably part of it, just let me pick a subject first, then what I do with it.

At another level I'm thinking... yea... if we have to go more than one level deep in indexed access, something is wrong.

I'd be game using a subject-verb-object form, even if it's a little dastardly.
