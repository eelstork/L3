using UnityEngine;

public static class BoundsExt{

    public static float Height(this Bounds self)
    => self.size.y;

    public static float Radius(this Bounds self)
    => Mathf.Max(self.extents.x, self.extents.z);

    public static float Radius3(this Bounds self)
    => self.extents.magnitude;

    public static float Radius(this Bounds self, bool planar){
        var e = self.extents;
        var r = e.x;
        if(!planar) r = Mathf.Max(r, e.y);
        r = Mathf.Max(r, e.z);
        return r;
    }

}
