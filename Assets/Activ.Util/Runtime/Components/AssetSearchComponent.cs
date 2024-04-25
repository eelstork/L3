using System.IO;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Activ.Util{
[ExecuteInEditMode]
public class AssetSearchComponent : MonoBehaviour{

    [Header("Input")]
    public string filter;
    public string[] folders;
    [Header("Output")]
    public bool trimPaths=true;
    public string[] output;
    int id;

    void Update(){
        if(id == evalId) return;
        var @out = folders.Length > 0
                ? AssetDatabase.FindAssets(filter, folders)
                : AssetDatabase.FindAssets(filter);
        output = @out.Select(
            x => Trim(AssetDatabase.GUIDToAssetPath(x))
        ).ToArray();
        string Trim(string arg) => trimPaths ? Path.GetFileName(arg)
                                             : arg;
    }

    int evalId{ get{
        var id = filter?.GetHashCode() ?? -1;
        if(folders != null){
            foreach(var folder in folders) id += folder.GetHashCode();
        }
        id += trimPaths ? 1 : 0;
        return id;
    }}

}}
