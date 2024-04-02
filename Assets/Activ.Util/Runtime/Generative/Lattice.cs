using UnityEngine; using v3 = UnityEngine.Vector3; using Activ.Util;

namespace Activ.Util.Generative{
[ExecuteInEditMode] public class Lattice : MonoBehaviour{

    public enum UpdatePolicy{ None, Now, Often }

    public int count = 3;
    public float scale = 1f, massScaling = 1f;
    public GameObject prefab; public string instanceName;
    public UpdatePolicy updatePolicy;
    public bool updateOnGameStart;

    void Start(){
        if(updateOnGameStart || !Application.isPlaying){
            transform.Clear(); Generate();
        }
    }

    void Update(){
        if(Application.isPlaying) return;
        switch(updatePolicy){
            case UpdatePolicy.Now: DoUpdate(UpdatePolicy.None); break;
            case UpdatePolicy.Often: DoUpdate(updatePolicy); break;
        }
        void DoUpdate(UpdatePolicy next)
        { transform.Clear(); Generate(); updatePolicy = next; }
    }

    void Generate(){
        if(!prefab) return;
        // NOTE: shifting origin by -0.5 compensates a block's
        // half-width assuming block size one.
        var halfsize = count / 2f; var orig = - v3.one * (halfsize - 0.5f);
        for(var y = count - 1; y >= 0; y--)
            for(var x = 0; x < count; x++)
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
