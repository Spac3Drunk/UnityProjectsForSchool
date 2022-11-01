using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuysSpawner : MonoBehaviour
{
    public GameObject BadPawn;
    public GameObject BadGuyHolder;
    
    GameObject BadGuy;

    void Awake(){
        spawnBadGuys();
        spawnBadGuys();
        spawnBadGuys();
        spawnBadGuys();
    }

    void FixedUpdate()
    {
        countBadPawn(BadGuyHolder);
    }

    public void spawnBadGuys(){
        float x = Random.Range(-10, 10);
        float y = Random.Range(-10, 10);
        BadGuy = Instantiate(BadPawn, new Vector3(x, 5, y), Quaternion.identity);
        BadGuy.transform.parent = BadGuyHolder.transform;
    }

    public void countBadPawn(GameObject obj){
        if (obj.transform.childCount < 4){
            spawnBadGuys();
        }
    }

}
