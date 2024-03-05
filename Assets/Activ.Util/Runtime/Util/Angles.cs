using UnityEngine;
using v3 = UnityEngine.Vector3;

namespace Activ.Util{
public static class Angles{

    // TODO - essentially the key function implied by this
    // should take an input vector and return an "iso"
    // direction, not let the user figure how to use this
    public static class Isometric{

        public static int CameraStop16(Camera cam=null){
            var angle = (float)CameraAngle(cam);
            var stop = (int)Mathf.Round((angle / 360) * 16);
            return stop % 16;
        }

        public static int CameraAngle(Camera cam=null){
            if(!cam) cam = Camera.main;
            var u = cam.transform.right;
            var angle = v3.SignedAngle(u, v3.right, v3.up);
            while(angle < 0f) angle += 360;
            return (int)angle;
        }

        public static float[] naviAngles16 = new float[16]{
               0,    0,  -45,  -90,  -90,  -90, -135, -180,
            -180, -180, -225, -260, -260, -260,   45,    0
        };

    }

}}
