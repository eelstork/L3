using System.Linq; using System.Collections.Generic;
using System.Reflection;
using static UnityEngine.Debug;
using Activ.Graphs;

public static class Solver{

    public static object Find(string type, object[] args){
        var output =
            BFS1.Find(Expand, args[0], x => Match(x, type));
        var path = string.Join(", ", output);
        //og($"SOLVER FOUND {path}");
        return output[output.Length - 1];
    }

    public static object[] Expand(object arg){
        //og($"Expand [{arg}]");
        var type = arg.GetType();
        var methods = type.GetMethods();
        var @out = new List<object>();
        foreach(var m in methods){
            var n = m.GetParameters().Length;
            if(n == 0){
                object output = null;
                try{
                    output = m.Invoke(arg, new object[]{});
                }catch(System.Exception){}
                if(output != null) @out.Add(output);
                //og($"Considered {type.Name}.{m.Name} => {output}");
                //@out.Add(new CsAction(m, arg));
            }
        }
        //og($"---------------");
        return @out.ToArray();
    }

    public static bool Match(object x, string type){
        if(x == null) return false;
        var tname = x.GetType().Name;
        if(tname == "Int32") tname = "int";
        //og($"Match [{x}] ({tname}) to type constraint [{type}]");
        if(tname == "int" && type == "uint"){
            var num = (int)x;
            //og($"Check int/uint ({num})");
            if(num > 0){
                //og("FOUND POSITIVE");
                return true;
            }else{
                return false;
            }
        }
        return type == tname;
    }

    class CsAction{
        MethodInfo m; object t;
        public CsAction(MethodInfo me, object ta){
            m = me; t = ta;
        }
    }

}
