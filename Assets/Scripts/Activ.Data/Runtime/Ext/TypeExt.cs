using System; using System.Reflection;
using InvOp = System.InvalidOperationException;

public static class TypeExt{

    public static Type FieldType(this Type self, string fieldName){
        var field = self.GetField(fieldName);
        if(field == null) throw new InvOp(
            $"No [{fieldName}] field in type [{self.Name}]"
        );
        return field.FieldType;
    }

    public static string Name(this Type self, out int gcount)
    => (gcount = self.GetGenericArguments().Length) == 0
       ? self.Name : self.Name.Split('`')[0];

}
