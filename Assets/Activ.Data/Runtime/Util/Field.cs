using System; using System.Reflection;

namespace Activ.Data.Util{
public class Field{

    object owner;
    FieldInfo field;
    public string name;
    public object value;
    public Type type;

    public Field(object v){
        name = null; value = v; type = v?.GetType();
    }

    public Field(FieldInfo field, object owner){
        this.field = field;
        this.owner = owner;
        name = field.Name;
        value = field.GetValue(owner);
        type = field.FieldType;
    }

    public void Assign(object value)
    => field.SetValue(owner, value);

    override public string ToString()
    => type.Name + ' ' + name + ": " + (value?.ToString() ?? "null");

}}
