using System; using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using static UnityEngine.Debug;

public class AssembliesTest{

    public static string[] hints = {
        "android", "bee.beedriver", "excss", "log4net", "mono",
        "mscorlib", "niceio", "nunit", "netstandard", "playerbuild",
        "test", "reportgen", "system", "unity", "webgl"
    };

    [Test] public void ListAssemblies(){
        // Get all loaded assemblies
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        // Iterate through each assembly
        foreach (Assembly assembly in assemblies)
        {
            // Display information about the assembly
            var name = new AssemblyName(assembly.FullName).Name;
            if(Skip(name)) continue;
            //if(name.StartsWith("Unity") || name.StartsWith("System"))
            //continue;
            Log($"Assembly: {name}");

            // Get all types in the assembly
            Type[] types = assembly.GetTypes();

            // Display information about each type
            foreach (Type type in types)
                Log(type.FullName);
        }
    }

    static bool Skip(string name)
    => Array.Exists(hints, x => name.ToLower().StartsWith(x));

}
