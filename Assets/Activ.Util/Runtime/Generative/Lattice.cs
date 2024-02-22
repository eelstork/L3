using UnityEngine; using v3 = UnityEngine.Vector3; using Activ.Util;

namespace Activ.Util.Generative{
[ExecuteInEditMode] public class Lattice : MonoBehaviour{

    public int count = 3;
    public float scale = 1f, massScaling = 1f;
    public GameObject prefab; public string instanceName;
    public bool updateNow;
    public bool updateOnGameStart;

    void Start(){
        if(updateOnGameStart || !Application.isPlaying){
            transform.Clear(); Generate();
        }
    }

    void Update(){
        if(updateNow){ transform.Clear(); Generate(); }
        updateNow = false;
    }

    void Generate(){
        if(!prefab) return;
        var halfsize = count/2f; var orig = - v3.one * halfsize;
        for(var x = 0; x < count; x++)
            for(var y = 0; y < count; y++)
                for(var z = 0; z < count; z++) Generate(x, y, z, orig);
    }

    void Generate(int x, int y, int z, v3 origin){
        var w = Instantiate(prefab, transform);
        w.transform.position = origin + new v3(x, y, z);
        if(scale != 0f) w.transform.localScale = v3.one * scale;
        if(instanceName.None()) w.name = $"C ({x}, {y}, {z})";
        else w.name = instanceName;
        if(massScaling != 1f)
        { var rb = w.transform.Rb(); rb.mass *= massScaling; }
        if(decorator != null) decorator.OnCreate(w, x, y, z);
    }

    LatticeDecorator decorator => GetComponent<LatticeDecorator>();

}}
