using UnityEngine;
using v3 = UnityEngine.Vector3;

namespace Activ.Util{
public static class Vector3ArrayExt{

    public static v3 Closest(this v3[] self, v3 u, float threshold){
        if(u.magnitude < threshold) return v3.zero;
        var max = float.MaxValue; var sel = v3.zero;
        foreach(var v in self){
            var a = v3.Angle(u, v); if(a < max){ sel = v; max = a; }
        } return sel;
    }

    public static v3[] Smooth(
        this v3[] self, int count=1, int fromIndex = 1
    ){
        if(self.Length < 3) return self;
        var buf = new v3[self.Length];
        for(var i = 0; i < count; i++){
            self.SafeSmooth(buf, fromIndex);
        } return self;
    }

    static void Smooth(this v3[] self, v3[] buf){
        var n = self.Length;
        for(var i = 1; i < n - 1; i++){
            buf[i] = (self[i - 1] + self[i] + self[i + 1]) / 3;
        }
        System.Array.Copy(buf, 1, self, 1, n - 2);
    }

    static void SafeSmooth(this v3[] self, v3[] buf, int fromIndex){
        var n = self.Length;
        for(var i = fromIndex; i < n - 1; i++){
            v3 A = self[i - 1], B = self[i], C = self[i + 1];
            var B1 = (A + B + C) / 3;
            buf[i] = Blocked(A, B1) || Blocked(B1, C) ? B : B1;
        }
        // NOTE - smooth only applies to [fromIndex, lastIndex - 1]
        // therefore we need to copy unprocessed elements
        var len = n - 1 - fromIndex;
        System.Array.Copy(buf, fromIndex, self, fromIndex, len);
    }

    static bool Blocked(v3 A, v3 B){
        var u = B - A;
        return Physics.Raycast(A, u, out RaycastHit _, u.magnitude);
    }

}}
