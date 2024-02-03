using UnityEngine;
namespace Axis{
public class Critter : MonoBehaviour{

    Task[] tasks;
    public int index;
    public string status;

    void OnEnable(){
        int n = 24;
        tasks = new Task[n + 1];
        for(int i = 0; i < n; i++){
            tasks[i] = CreateTask();
        }
        tasks[n] = new Drop();
    }

    Task CreateTask(){
        switch(Random.Range(0, 5)){
            case 0: return new Move();
            case 1: return new Move();
            case 2: return new Grab();
            case 3: return new DropAt();
            case 4: return new Visit();
        }
        return null;
    }

    void Update(){
        var task = tasks[index];
        status = index + ": " + task.ToString();
        bool done = task.Exe(transform);
        if(done) index = (index + 1) % tasks.Length;
    }

}}
