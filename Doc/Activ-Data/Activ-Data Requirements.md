# Activ-Data requirements

(1) We need to know what we can and cannot write/read

The situations we are trying to avoid:
(a) user create a model, which serializes *some* data, but there is editable data which does not get serialized.
(b) user create a model which serializes data; later the data cannot be read.
(c) the model serializes uneditable data, which is not seen in the inspector.

(2) Lacking support for model migration

The situation we are trying to avoid: data loss.
