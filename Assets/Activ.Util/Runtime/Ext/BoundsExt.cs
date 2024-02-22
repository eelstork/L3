using UnityEngine;

public static class BoundsExt{

    public static float Radius(this Bounds self, bool planar){
        var e = self.extents;
        var r = e.x;
        if(!planar) r = Mathf.Max(r, e.y);
        r = Mathf.Max(r, e.z);
        return r;
    }

}
