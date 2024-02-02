using System; using System.Reflection;

public class Assemblies
{
    static void Main()
    {
        // Get all loaded assemblies
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

        // Iterate through each assembly
        foreach (Assembly assembly in assemblies)
        {
            // Display information about the assembly
            Console.WriteLine($"Assembly: {assembly.FullName}");

            // Get all types in the assembly
            Type[] types = assembly.GetTypes();

            // Display information about each type
            foreach (Type type in types)
            {
                Console.WriteLine($"Type: {type.FullName}");
            }
        }
    }
    
}
