using UnityEngine;

namespace Activ.Util{
public class DestroyLater : MonoBehaviour{

    public float delay = 1f;

    void Start() => Invoke("Apply", delay);

    void Apply() => Destroy(gameObject);

}}
