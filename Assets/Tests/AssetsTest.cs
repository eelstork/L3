using NUnit.Framework;
using UnityEngine; using static UnityEngine.Debug; using UnityEditor;
using Activ.Util;

public class AssetsTest{

    [Test] public void CreateDir(){
        Assets.MakeDir("TestDir/OuterDir/InnerDir");
        Assets.MakeDir("TestDir/OuterDir/InnerDir");        
        AssetDatabase.DeleteAsset("Assets/TestDir/OuterDir/InnerDir");
        AssetDatabase.DeleteAsset("Assets/TestDir/OuterDir");
        AssetDatabase.DeleteAsset("Assets/TestDir");
    }

}
