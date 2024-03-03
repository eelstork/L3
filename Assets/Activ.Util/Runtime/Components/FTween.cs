using System; using System.Collections.Generic; using System.Linq;
using UnityEngine; using v3 = UnityEngine.Vector3;

namespace Activ.Util{
public class FTween : MonoBehaviour{

    List<Func<v3>> waypoints;
    public float offset;

    public void Init(IEnumerable<Func<v3>> w) => waypoints = w.ToList();

    void Update(){
        offset += Time.deltaTime;
        while(offset > 1f){
            waypoints.RemoveAt(0);
            offset -= 1f;
        }
        if(waypoints.Count < 2){
            pos = waypoints[0](); Destroy(this); return;
        }
        pos = v3.Lerp(waypoints[0](), waypoints[1](), offset);
    }

    v3 pos{
        get => transform.position;
        set => transform.position = value;
    }

}}
