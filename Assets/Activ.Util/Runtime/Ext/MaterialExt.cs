using UnityEngine;
using Mat = UnityEngine.Material;

namespace Activ.Util{
public static class MaterialExt{

    public static Mat WithColor(this Mat self, Color col, string name){
        var x = new Material(self.shader);
        x.name = name;
        x.CopyPropertiesFromMaterial(self);
        x.color = col;
        return x;
    }

}}
