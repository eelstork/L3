using System; using System.Collections.Generic;
using System.Linq;
using System.Reflection; using System.Reflection.Emit;
using static System.Reflection.BindingFlags;
using Mem = System.Reflection.MemberInfo;
using Met = System.Reflection.MethodInfo;
using Fie = System.Reflection.FieldInfo;
using Pro = System.Reflection.PropertyInfo;

public static class TypeExt{

    public static bool Accept(this Type self, MemberInfo arg){
        if(self == null) return true;
        if(arg == null) return true;
        var argtype = arg switch{
            Fie f => f.FieldType,
            Pro p => p.PropertyType,
            _ => null
        };
        if(argtype.IsAssignableFrom(self)){
            return true;
        }else{
            //UnityEngine.Debug.Log($"{self} does not accept {arg}");
            return false;
        }
    }

    public static IEnumerable<Met> DecMethods(this Type self) => (
        from m in self.GetMethods(Instance | Public | DeclaredOnly)
        where !DenotesAccessor(m.Name) select m
    );

    public static Mem[] DecFieldsOrProps(this Type self, Type k=null){
        var fields = self.DecFields(k);
        var props = self.DecProps(k);
        var m = new List<Mem>();
        m.AddRange(fields); m.AddRange(props); return m.ToArray();
    }

    public static Fie[] DecFields(this Type self, Type k) => (
        from f in self.DecFields() where k.Accept(f) select f
    ).ToArray();

    public static Pro[] DecProps(this Type self, Type k) => (
        from x in self.DecProps() where k.Accept(x) select x
    ).ToArray();

    public static Pro[] DecProps(this Type self)
    => (
        from x in self.GetProperties(Instance | Public | DeclaredOnly)
        select x
    ).ToArray();

    public static Fie[] DecFields(this Type self) => (
        from f in self.GetFields(Instance | Public | DeclaredOnly)
        select f
    ).ToArray();

    static bool DenotesAccessor(string arg)
    => arg.StartsWith("get_") || arg.StartsWith("set_");

}
