using System; using System.Collections.Generic;
using System.Reflection;
using InvOp = System.InvalidOperationException;

namespace Activ.Util{
public static class Types{

    public static string[] hints = {
        "android", "bee.beedriver", "excss", "log4net", "mono",
        "mscorlib", "niceio", "nunit", "netstandard", "playerbuild",
        "test", "reportgen", "system", "unity", "webgl"
    };

    static Dictionary <string, Type> _types;
    static Dictionary <string, Type> custom;

    public static Type FindOld(string name){
        if(_types == null) Init();
        if(!_types.ContainsKey(name)) return null;
        return _types[name];
    }

    public static Type Find(string name)
    => custom.Get(name)
    ?? _types.Get(name)
    ?? throw new InvOp($"C# type not found: {name}");

    public static Type FindOrDefault(string name)
    => custom.Get(name) ?? _types.Get(name) ?? null;

    public static void SetCustomTypes(Dictionary<string, Type> types){
        custom = types;
    }

    public static void SetCustomTypes(params Type[] _types){
        custom = new ();
        foreach(var k in _types) custom[k.Name] = k;
    }

    static void Init(){
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        _types = new ();
        foreach (var ass in assemblies){
            var name = new AssemblyName(ass.FullName).Name;
            if(Skip(name)) continue;
            foreach(var type in ass.GetTypes()){
                if(type.HasDefaultConstructor()){
                    _types[type.Name] = type;
                }
            }
        }
    }

    static bool Skip(string name)
    => Array.Exists(hints, x => name.ToLower().StartsWith(x));

    static Dictionary <string, Type> types{ get{
        if(_types == null) Init();
        return _types;
    }}

}}
