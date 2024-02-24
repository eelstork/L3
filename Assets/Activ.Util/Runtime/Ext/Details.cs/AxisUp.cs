using UnityEngine;

namespace Activ.Util{
internal static class AxisUp{

    public static void Y(Transform self)
    {
        // Get the current up direction of the cube
        Vector3 upDirection = self.up;

        // Define the target up direction (which is world up)
        Vector3 targetUpDirection = Vector3.up;

        // Calculate the rotation needed to align the Y axis with the target up direction
        Quaternion rotation = Quaternion.FromToRotation(upDirection, targetUpDirection);

        // Convert the rotation into a series of 90 degree rotations
        rotation = SnapRotationTo90Degrees(rotation);

        // Apply the rotation to the cube's transform
        self.rotation = rotation * self.rotation;
    }

    static Quaternion SnapRotationTo90Degrees(Quaternion rotation)
    {
        // Get the euler angles of the rotation
        Vector3 euler = rotation.eulerAngles;

        // Round the angles to the nearest 90 degrees
        euler.x = Mathf.Round(euler.x / 90) * 90;
        euler.y = Mathf.Round(euler.y / 90) * 90;
        euler.z = Mathf.Round(euler.z / 90) * 90;

        // Convert the euler angles back to a quaternion
        return Quaternion.Euler(euler);
    }

}}
