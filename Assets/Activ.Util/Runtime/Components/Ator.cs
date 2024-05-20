using UnityEngine;

namespace Activ.Util{
public class Ator : MonoBehaviour{

    Animator _cached;

    public Animator animator => x;

    public RuntimeAnimatorController ac => x.runtimeAnimatorController;

    public bool rootMotion
    { get => x.applyRootMotion; set => x.applyRootMotion = value; }

    public void SetBool(string key, bool flag){
        if(x.GetBool(key) == flag) return;
        //Debug.Log($"Set {key}: {flag} (t: {Time.time:0.00})", this);
        x.SetBool(key, flag);
    }

    Animator x
    => _cached ?? (_cached = GetComponentInChildren<Animator>());

}}
