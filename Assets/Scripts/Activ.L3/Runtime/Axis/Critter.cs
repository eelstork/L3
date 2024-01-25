using UnityEngine;
namespace Axis{
public class Critter : MonoBehaviour{

    Task[] tasks;
    public int index;
    public string status;

    void OnEnable(){
        int n = 3;
        tasks = new Task[n + 1];
        for(int i = 0; i < n; i++){
            tasks[i] = new Move();
        }
        tasks[n] = new Home();
    }

    void Update(){
        var task = tasks[index];
        status = task.ToString();
        bool done = task.Exe(transform);
        if(done) index = (index + 1) % tasks.Length;
    }

}}
