using UnityEngine;

namespace Activ.Util{
public class TargetPoint : MonoBehaviour, IPoint3{

    public Vector3 target;

    Vector3 IPoint3.position => target;

}}
