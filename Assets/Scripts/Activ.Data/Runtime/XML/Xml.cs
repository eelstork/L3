using System;
using InvOp = System.InvalidOperationException;

namespace Activ.XML{
public static class Xml{

    public static void Read(
        string arg, object target, bool ignoreErrors
    )
    => new XmlReader(ignoreErrors).Read(arg, target);

    public static T Read<T>(string arg){
        var reader = new XmlReader(ignoreErrors: false);
        var value = reader.Read(arg, typeof(T));
        if(value is T){
            return (T)value;
        }else{
            var expected = typeof(T).Name;
            var found = value.GetType().Name;
            throw new InvOp($"Expected {expected}, found {found}");
        }
    }

    public static object Read(string arg, Type type=null)
    => new XmlReader(ignoreErrors: false).Read(arg, type);

}}
