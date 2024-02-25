using UnityEngine;

namespace Activ.Util{
public class TargetTransform : MonoBehaviour, IPoint3{

    public Transform target;

    Vector3 IPoint3.position => target.position;

}}
