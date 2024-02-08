using UnityEngine;

namespace L3{
[CreateAssetMenu(fileName = "L3Script.asset", menuName = "L3 Script")]
public class L3Script : Activ.Data.Model{

    [System.NonSerialized]
    public Unit value = new Unit();

    public Unit unit => value;

}}
