using System;
//using System.Reflection;

[AttributeUsage(
    AttributeTargets.Field,
    Inherited = false,
    AllowMultiple = false
)]
public class HierarchyAttribute : Attribute {}
