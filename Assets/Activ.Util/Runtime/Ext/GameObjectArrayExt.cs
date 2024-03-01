using UnityEngine;

public static class GameObjectArrayExt{

    public static void Hide(this GameObject[] self){
        self.SetActive(false);
    }

    public static void SetActive(this GameObject[] self, bool flag){
        foreach(var k in self) k.SetActive(flag);
    }

    public static void Show(this GameObject[] self){
        self.SetActive(true);
    }

}
