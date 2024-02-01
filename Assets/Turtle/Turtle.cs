using UnityEngine;
using System.Collections;

public class Turtle : MonoBehaviour{

    void OnEnable(){
        StartCoroutine(Draw());
    }

    IEnumerator Draw(){
        for(var i = 0; i < 100; i++){
            for(var j = 10; j < 100; j+=3){
                yield return Forward(i - j);
                Right(j);
            }
            //Left(20);
        }
        yield return null;
    }

    IEnumerator Forward(int steps){
        for(int i = 0; i < steps; i++){
            self.position += self.forward * 0.01f;
            yield return null;
        }
    }

    void Right(float angle){
        self.Rotate(0, angle, 0);
    }

    Transform self => transform;

}
