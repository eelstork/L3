# General issues

## Interop between foreign assets

When importing an element, only the type name is checked, disregarding the originating namespace.

Within a given asset or project, expecting a developer to use unique type names is not unreasonable. Across solutions, conflicts will arise.

TODO when reading, the developer can specify one or several namespaces to match against.

## Manually adding types to the "dictionary"

## Performance when deserializing through the dictionary

# Cosmetic issues

(*) For primitive types, we would prefer "int" vs "Int32" (and the like...)

(*) For strings, we would prefer skipping the 't' attribute; does not apply to enumerations

(*) For primitive type arrays, a more concise notation may be preferred:

```xml
<int-Array>1, 2, 3</int-Array>
```

```xml
<Int32-Array>
    <Int32>1</Int32>
    <Int32>2</Int32>
    <Int32>3</Int32>
</Int32-Array>;
```
