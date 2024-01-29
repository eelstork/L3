using UnityEngine;
using UnityEditor;
using System.Linq;
using static UnityEditor.AssetDatabase;

namespace Activ.Util{
public static class Assets{

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
