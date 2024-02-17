using System.Collections.Generic;
using InvOp = System.InvalidOperationException;
using UnityEngine;

namespace Activ.Util{
public class CollisionTracker : MonoBehaviour{

    HashSet<Collider> S = new ();

    // NOTE: on its own, a collider does not detect collisions,
    // even if an rb colliding with the collider detects collisions
    // on 'their' side.
    void Start(){
        var c = GetComponent<Collider>();
        var s = GetComponent<Rigidbody>();
        if(!c || !s) Debug.LogError(
            "Tracker will not detect collisions"
        );
    }

    public bool IsContacting(Transform x, bool @unsafe) => @unsafe
        ? S.Contains(x.GetComponent<Collider>())
        : throw new InvOp("Not implemented");

    void OnCollisionEnter(Collision x) => S.Add(x.collider);

    void OnCollisionStay(Collision x) => S.Add(x.collider);

    void OnCollisionExit(Collision x) => S.Remove(x.collider);

}}
