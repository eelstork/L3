# "Using" clauses

In a first approximation, using clauses can be implemented through running supporting modules.

When starting execution of an L3 component, we first enter the root scope; this is done before executing any unit at all.

When importing a namespace, we iterate units (script files); units partaking the namespace are executed first. While this process is recursive, the same unit is not running twice (should not...)

When running a unit, we do not enter a scope. That's because, consistently with 'using', entered namespace are treated as "in-scope".

All told this logic does have limits. If A uses B and B uses C, ideally we would prefer A not to see C. I will not fix this now but, eventually... needs addressing.
