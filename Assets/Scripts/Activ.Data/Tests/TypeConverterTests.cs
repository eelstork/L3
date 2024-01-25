using System;
using System.Collections.Generic;
using System.ComponentModel;
using NUnit.Framework;
using static UnityEngine.Debug;

public class TypeConverterTest{

    [Test] public void ConvertIntArray(){
        var array = new int[]{1, 2, 3};
        var type = array.GetType();
        var c = TypeDescriptor.GetConverter(type);
        //Log($"Converter for {type.Name}: {c}");
        Log(c.ConvertToString(array));
    }

}
