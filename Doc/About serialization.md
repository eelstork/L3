# About serialization

## Reading and writing lists and array

When writing lists and arrays, we iterate children and write each as a separate element. Currently lists and arrays are captured via `IEnumerable`, which may be too broad.

When reading subelements, child elements are implied because they do not have a 'field-name' attribute. For a list, we then cast the parent object to `dynamic`, which allows calling List.Add with generic lists, without knowing the specific (type-parameterized) type of the list.

Before reading into an array, we need to verify the element count.
