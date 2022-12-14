using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BadGuys : MonoBehaviour
{
    public float hp = 3;
    //private float xDest;
    //private float yDest;
    //private float timer;

    public GameObject targetObject;
    [SerializeField] private Transform targetPos;
    private NavMeshAgent navMeshAgent;

    //private Quaternion _lookRotation;
    //private Vector3 _direction;

    public AudioClip deathClip;

    public GameObject loot1;
    public GameObject loot2;
    private Transform lastDmgPos;

    private bool hasMarkScore = false; //debug :D

    void Awake(){
        navMeshAgent = GetComponent<NavMeshAgent>();
        targetObject = GameObject.FindGameObjectsWithTag("Player")[0];
        targetPos = targetObject.transform;
    }

    void FixedUpdate(){
        takeDmg(0.000f); //check if die
        if(navMeshAgent.isOnNavMesh)
        {
            navMeshAgent.destination = targetPos.position;
        }
        if (Vector3.Distance(this.transform.position,targetPos.position) < 1.4)
        {
            GameState.getHit();
        }
        //checkRoute();
        //Debug.Log(Time.timeScale);
    }
/*
    private void newRoute(){
        xDest = Random.Range(-20, 20);
        yDest = Random.Range(-20, 20);
        timer = Random.Range(0, 10);
        targetPos = new Vector3(xDest, 5, yDest);

        _direction = (targetPos - transform.position).normalized;
        _lookRotation = Quaternion.LookRotation(_direction);
    }

    private void checkRoute(){
        timer -= Time.deltaTime;
        if (timer < 0 || ((transform.position.x-xDest)*(transform.position.x-xDest) < 1 && (transform.position.x-xDest)*(transform.position.y-yDest) < 1)){
            newRoute();
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 10f);
        transform.position += transform.right * Time.deltaTime * 2f;
        if (transform.position.y < 5){
            transform.position += Vector3.up * Time.deltaTime;
        }
        if (transform.position.y > 5){
            transform.position -= Vector3.up * Time.deltaTime * 0.5f;
        }
        if (transform.position.y > 7){
            transform.position -= Vector3.up * Time.deltaTime * 20f;
        }
    }
*/
    public void takeDmg(float Damage){
        lastDmgPos = this.transform;
        hp = hp - Damage;
        if(hp < 0){
            die();
        }
    }

    public void die(){
        SoundManager.instance.PlaySound(deathClip);
        if (!hasMarkScore){ // to make sure that you mark score once per kill
            hasMarkScore = true;
            GameState.markScore();
            if (Random.Range(0.0f, 1.0f) < 0.1f){
                Instantiate(loot1, lastDmgPos.position + new Vector3(0, 1, 0), Quaternion.identity);
            }
            if (Random.Range(0.0f, 1.0f) < 0.1f){
                Instantiate(loot2, lastDmgPos.position + new Vector3(0, 1, 0), Quaternion.identity);
            }
        }
        //Add a little delay, just to make sure everything works fine
        Invoke("selfDestroy", 0.05f);
    }

    private void selfDestroy(){
        Destroy(gameObject);
    }
}
