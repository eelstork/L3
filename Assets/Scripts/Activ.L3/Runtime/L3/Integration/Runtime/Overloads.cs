using System;
using System.Reflection;
using System.Linq;

public class MyClass
{
    public void MyMethod(int param1, string param2)
    {
        Console.WriteLine($"MyMethod called with param1: {param1}, param2: {param2}");
    }

    public void MyMethod(string param)
    {
        Console.WriteLine($"MyMethod called with param: {param}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create an instance of the target object
        MyClass myObject = new MyClass();

        // Method name
        string methodName = "MyMethod";

        // Parameters
        object[] parameters = { 10, "Hello" }; // Example parameters

        // Get the method overloads
        MethodInfo[] methods = typeof(MyClass).GetMethods()
            .Where(m => m.Name == methodName && m.GetParameters().Length == parameters.Length)
            .ToArray();

        // Find the matching overload
        MethodInfo methodInfo = null;
        foreach (var method in methods)
        {
            var methodParams = method.GetParameters();
            bool paramsMatch = true;
            for (int i = 0; i < methodParams.Length; i++)
            {
                if (parameters[i] != null && parameters[i].GetType() != methodParams[i].ParameterType)
                {
                    paramsMatch = false;
                    break;
                }
            }

            if (paramsMatch)
            {
                methodInfo = method;
                break;
            }
        }

        if (methodInfo != null)
        {
            // Invoke the method dynamically
            methodInfo.Invoke(myObject, parameters);
        }
        else
        {
            Console.WriteLine("Method not found!");
        }
    }
}
