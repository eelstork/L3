using UnityEngine;

namespace Activ.Util{
[CreateAssetMenu(
    fileName = "List",
    menuName = "Activ Data/List", order = 2
)]
public class ListData : ScriptableObject{

    public string[] elements;

    public static implicit operator string[](ListData self)
    => self?.elements ?? null;

}}
