using System.Reflection;

namespace R1{
public static class ObjectExt{

    public static object GetFieldOrPropertyValue(
        this object self, string name, out bool found
    ){
        var type = self.GetType();
        var field = type.GetField(name);
        if(field != null){
            found = true;
            return field.GetValue(self);
        }
        var prop = type.GetProperty(name);
        if(prop != null){
            found = true;
            return prop.GetValue(self);
        }
        found = false;
        return null;
    }

}}
