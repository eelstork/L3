using System; using System.Collections.Generic;
using System.Reflection;

public static class Types{

    public static string[] hints = {
        "android", "bee.beedriver", "excss", "log4net", "mono",
        "mscorlib", "niceio", "nunit", "netstandard", "playerbuild",
        "test", "reportgen", "system", "unity", "webgl"
    };

    static Dictionary <string, Type> types;

    public static Type Find(string name){
        if(types == null) Init();
        return types.ContainsKey(name) ? types[name] : null;
    }

    static void Init(){
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        types = new ();
        foreach (var ass in assemblies){
            var name = new AssemblyName(ass.FullName).Name;
            if(Skip(name)) continue;
            foreach(var type in ass.GetTypes()){
                if(type.HasDefaultConstructor()){
                    types[type.Name] = type;
                }
            }
        }
    }

    static bool Skip(string name)
    => Array.Exists(hints, x => name.ToLower().StartsWith(x));

}
