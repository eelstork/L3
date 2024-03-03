using System; using System.Collections.Generic; using System.Linq;
using UnityEngine; using v3 = UnityEngine.Vector3;
using q4 = UnityEngine.Quaternion;

namespace Activ.Util{
public class FTweenPosRot : MonoBehaviour{

    List<Func<(v3, q4)>> waypoints;
    public Action onEnd;
    [Header("Info")]
    public float offset;

    public void Init(Action onEnd, IEnumerable<Func<(v3, q4)>> w){
        waypoints = w.ToList();
        this.onEnd = onEnd;
    }

    void Update(){
        offset += Time.deltaTime;
        while(offset > 1f){
            waypoints.RemoveAt(0);
            offset -= 1f;
        }
        if(waypoints.Count < 2){
            var w = waypoints[0]();
            pos = w.Item1; rot = w.Item2;
            onEnd();
            Destroy(this); return;
        }
        var w0 = waypoints[0](); var w1 = waypoints[1]();
        pos = v3.Lerp(w0.Item1, w1.Item1, offset);
        rot = q4.Lerp(w0.Item2, w1.Item2, offset);
    }

    v3 pos{
        get => transform.position;
        set => transform.position = value;
    }

    q4 rot{
        get => transform.rotation;
        set => transform.rotation = value;
    }

}}
