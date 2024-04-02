#if UNITY_EDITOR
using UnityEngine; using UnityEditor; using System.Linq;
using static UnityEditor.AssetDatabase;

namespace Activ.Util{
public static class Assets{

    // Create folder in assets (recursive)
    // Differences with AssetDatabase.CreateFolder()
    // - 'Assets/' is assumed. Therefore CreateFolder("Content")
    // will create the 'Assets/Content' directory.
    // - Will not create 'Dir 1' if 'Dir' already exists
    public static void MakeDir(string path){
        path = path.Trim();
        if(string.IsNullOrEmpty(path)) return;
        var dirs = path.Split('/');
        var parent = "Assets";
        foreach(var dir in dirs){
            //ebug.Log($"Create dir {parent}");
            if(!System.IO.Directory.Exists(parent)){
                AssetDatabase.CreateFolder(parent, dir);
            }else{
                //ebug.Log($"Already exists: {parent}");
            }
            parent += "/" + dir;
        }
    }

    public static T FindEditorResource<T>(string key=null)
    where T : Object{
        var name = EditorPrefs.GetString(key);
        return name == null ? null : Find<T>(name);
    }

    public static T[] FindAll<T>(string name=null) where T : Object{
        var guids = FindAssets($"{name} t:{typeof(T)}");
        return (from x in guids select ToAsset<T>(x)).ToArray();
    }

    public static T Find<T>(string name=null) where T : Object{
        var guids = FindAssets($"{name} t:{typeof(T)}");
        return guids.Length == 0 ? null : ToAsset<T>(guids[0]);
    }

    //public static T[] FindAll<T>(string name=null) where T : Object
    //=> FindAssets($"t:{typeof(T)}").Via( ToAsset<T> );

    static T ToAsset<T>(string guid) where T : Object
    => LoadAssetAtPath<T>(GUIDToAssetPath(guid));

}}
#endif
