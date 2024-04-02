using Activ.Util.BT;

namespace L3{
public static class Statuses{

    public static bool IsDoneStatus(object arg){
        switch(arg){
            case true: return true;
            case Token.@void: return true;
            case status.done: return true;
        }
        return false;
    }

    public static bool IsDone(object arg){
        switch(arg){
            case true: return true;
            case Token.@void: return true;
            case status.done: return true;
            case null: return false;
        }
        return false;
    }

    public static bool IsCont(object arg){
        switch(arg){
            case Token.@cont: return true;
            case status.cont: return true;
        }
        return false;
    }

    public static bool IsFailing(object arg){
        switch(arg){
            case false: return true;
            case null: return true;
        }
        return false;
    }

}}
