using System;
using System.Collections.Generic;
using InvOp = System.InvalidOperationException;

public static class PrimitiveType{

    public static object FromString(string type, string x)
    => type switch{
        "Boolean" => bool.Parse(x),
        "Byte" => byte.Parse(x),
        "SByte" => sbyte.Parse(x),
        "Int16" => int.Parse(x),
        "Int32" => int.Parse(x),
        "Int64" => int.Parse(x),
        "UInt16" => uint.Parse(x),
        "UInt32" => uint.Parse(x),
        "UInt64" => uint.Parse(x),
        "Char" => char.Parse(x),
        "Double" => double.Parse(x),
        "Single" => float.Parse(x),
        "IntPtr" => throw new InvOp("IntPtr is not supported"),
        "UIntPtr" => throw new InvOp("UIntPtr is not supported"),
        _ => null
    };

}
