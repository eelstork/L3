using System;
using UnityEngine; using UnityEditor.Animations;
using State = UnityEditor.Animations.AnimatorState;
using Transition = UnityEditor.Animations.AnimatorStateTransition;
using SMB = UnityEngine.StateMachineBehaviour;

namespace Activ.Util{
public static class AnimatorStateExt{

    public static T Req<T>(this State self) where T : SMB{
        foreach(SMB smb in self.behaviours) if(smb is T){
            return smb as T;
        }
        return self.AddStateMachineBehaviour<T>();
    }

    public static void Transition(
        this State self, State to, string via, bool @set = true,
        bool bidi=false
    ){
        if(!self.HasTransitionTo(to)){
            var t = self.AddTransition(to);
            t.AddCondition(
                @set ? AnimatorConditionMode.If
                     : AnimatorConditionMode.IfNot,
                threshold: 0, parameter: via
            );
        }
        if(bidi) to.Transition(to: self, via: via, @set: false);
    }


    public static bool HasTransitionTo(this State self, State next)
    => self.FindTransitionTo(next) != null;

    public static Transition FindTransitionTo(
        this State self, State next
    ){
        foreach(var transition in self.transitions){
            if(transition.destinationState == next){
                return transition;
            }
        }
        return null;
    }

}}
