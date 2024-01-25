using System; using System.Collections.Generic; using System.Linq;
using System.Reflection;
using Activ.Data.Util;

namespace Activ.Data{
public static class ObjectEx{

    static Field[] NoFields = {};

    public static Field[] Fields(
        this object self
    ){
        if(self == null) return NoFields;
        var type = self.GetType();
        var fields = type.GetFields();
        var props = type.GetProperties();
        var count = fields.Length; //+ props.Length;
        var @out = new List<Field>(count);
        foreach(var x in fields){
            if(x.IsStatic) continue;
            @out.Add(new (field: x, owner: self));
        }
        /*
        foreach(var x in props){
            if(x.GetGetMethod().IsStatic) continue;
            @out.Add(new (
                x.Name, GetValue(self, x), x.PropertyType
            ));
        }
        */
        return @out.ToArray();
    }

    public static object GetValue(
        object x, System.Reflection.PropertyInfo prop
    ){
        try{
            return prop.GetValue(x);
        }catch(Exception){
            return null;
        }
    }

}}
