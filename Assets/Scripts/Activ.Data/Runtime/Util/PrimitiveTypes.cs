using System;
using System.ComponentModel;

public static class PrimitiveType{

    static Type[] primitives = {
        typeof(bool), typeof(byte), typeof(sbyte), typeof(char),
        typeof(short), typeof(ushort), typeof(int), typeof(uint),
        typeof(long), typeof(ulong), typeof(float), typeof(double),
        typeof(decimal), typeof(IntPtr), typeof(UIntPtr)
    };

    public static object FromString(string type, string value){
        var T = Array.Find(primitives, x => x.Name == type);
        if(T == null) return null;
        var c = TypeDescriptor.GetConverter(T);
        return c.ConvertFromString(value);
    }

}
