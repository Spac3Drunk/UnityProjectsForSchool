using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuysSpawner : MonoBehaviour
{
    public GameObject BadPawn;
    public GameObject BadGuyHolder;
    
    public GameObject[] spawnObject;
    [SerializeField] private Transform spawnPos;
    
    GameObject BadGuy;

    void Awake(){
        spawnObject = GameObject.FindGameObjectsWithTag("Spawner");
    }

    void FixedUpdate()
    {
        countBadPawn(BadGuyHolder);
    }

    public void spawnBadGuys(){
        spawnPos = spawnObject[Random.Range(0, spawnObject.Length)].transform;
        float x = Random.Range(-3.5f, 3.5f);
        float y = Random.Range(-3.5f, 3.5f);
        BadGuy = Instantiate(BadPawn, spawnPos.position + new Vector3(x, 0, y), Quaternion.identity);
        BadGuy.transform.parent = BadGuyHolder.transform;
    }

    public void countBadPawn(GameObject obj){
        if (!GameState.inDialogue && !GameState.dead) {
            if (obj.transform.childCount < 25) {
                spawnBadGuys();
            }
        }
    }

}
