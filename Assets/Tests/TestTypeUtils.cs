using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using static UnityEngine.Debug;
using static Activ.Data.Traversal;
using Activ.Util;

public class TestTypeUtils{

    [Test] public void FindCustomType(){
        Types.SetCustomTypes(
            new Dictionary<string, Type>(){
                {"Vector3", typeof(UnityEngine.Vector3)}
            }
        );
        Types.Find("Vector3");
    }

}
