# Fixing the stack

Currently when entering a (variable) scope we push a new scope to the frame. Therefore the structure is like this:

=========================
Stack (the store)
    Stack (the "frame")
        Scope ("local")
=========================
