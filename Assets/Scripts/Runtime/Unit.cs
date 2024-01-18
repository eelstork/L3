using UnityEngine;
using System.Collections.Generic;

namespace L3{
[CreateAssetMenu(
    fileName = "Unit.asset",
    menuName = "L3/Unit")
] public class Unit : ScriptableObject{

    public Function func =  new Function();

}}
