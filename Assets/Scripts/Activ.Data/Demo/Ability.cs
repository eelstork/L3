using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Activ.Data;

[CreateAssetMenu(fileName = "Ability.asset", menuName = "Activ-Data/Ability")]
public class Ability : Model{

    public string trigger;
    public INavigation navigation;
    public List<Effect> effects;

}
