using System;

[AttributeUsage(
    AttributeTargets.Method,
    Inherited = false,
    AllowMultiple = false
)]
public class EditorActionAttribute : Attribute {}
