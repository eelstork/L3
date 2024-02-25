using UnityEngine;
using v3 = UnityEngine.Vector3;

namespace Activ.Util{
public class TargetInterpolator: MonoBehaviour, IPoint3{

    public Transform start;
    public Transform end;
    public float smooth = 0.01f;
    [Header("Info")]
    public v3 current;

    Vector3 IPoint3.position => current;

    void Start() => current = start.position;

    void Update(){
        current = v3.Lerp(current, end.position, smooth);
    }

}}
