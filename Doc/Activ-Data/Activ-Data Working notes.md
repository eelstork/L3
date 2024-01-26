# Working notes

## Integrating with 'Component' and 'ScriptableObject'

While integrating custom serialization is not difficult, right now this is falling a little short.

First, about how Unity expects a developer to *integrate* with their serialization system, the TLDR:

- Transform data into something they can serialize.
- Expose the resulting data as a Unity-serialization friendly field.

What could go wrong? Well... depends.

### Approach 1 - pass the unity object itself to our serializer

This is actually the second approach experimented with, and what's currently in use. There are gotchas.

The 'unity friendly' data is stored as an XML string. This would bug our own serializer, as they would traverse the field. Easily solved using a private field with [SerializeField, HideInInspector]

However two issues remain.

(1) The string form XML stays in memory
Hoping that this will be avoided in builds since I do void the string after deserializing.

(2) Activ-Data fields "should" be marked as non-serialized; if they are not, Unity will attempt serializing the fields.
In general double-serialization is at best a non issue, and at worse a performance issue.
The main downside, currently, is that labeling XML-serialized fields as "non serialized" is counter-intuitive.



### Approach 2 - pass a 'value' object to our serializer
