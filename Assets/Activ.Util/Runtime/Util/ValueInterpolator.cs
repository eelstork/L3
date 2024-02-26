using System;
using UnityEngine;

// cam target 5, lerp 0.003
namespace Activ.Util{
[Serializable] public class ValueInterpolator{

    public float target = 1f;
    public float lerp = 0.1f;
    public bool enabled = true;

    public ValueInterpolator(float t, float l){
        target = t; lerp = l;
    }

    public float Tween(ref float val) => enabled ?
        val = Mathf.Lerp(val, target, lerp) : val;

    public static implicit operator ValueInterpolator(float val)
    => new ValueInterpolator(val, 0.1f);

}}
