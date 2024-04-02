#if UNITY_EDITOR
using UnityEngine;
using UnityEditor.Animations;
using Machine = UnityEditor.Animations.AnimatorStateMachine;
using State = UnityEditor.Animations.AnimatorState;

namespace Activ.Util{
public static class AnimatorStateMachineExt{

    public static State Req(this Machine self, string state)
    => self.Find(state) ?? self.AddState(state);
        //var s = self.Find(state);
        //return s == null ? self.AddState(state) : s;
    //}

    public static State Find(this Machine self, string stateName){
        foreach(var child in self.states){
            if(child.state.name == stateName) return child.state;
        }
        return null;
    }

}}

#endif
