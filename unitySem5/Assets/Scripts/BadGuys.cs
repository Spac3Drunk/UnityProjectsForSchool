using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuys : MonoBehaviour
{
    public float hp = 3;
    private float xDest;
    private float yDest;
    private float timer;
    private Vector3 targetPos;

    private Quaternion _lookRotation;
    private Vector3 _direction;

    void Awake(){
        newRoute();
    }

    void Update(){
        takeDmg(0.000f); //check if die
        checkRoute();
        //Debug.Log(hp);
    }

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

    public void takeDmg(float Damage){
        hp = hp - Damage;
        if(hp < 0){
            die();
        }
    }

    public void die(){
        //Add a little delay, just to make sure everything works fine
        Invoke("selfDestroy", 0.05f);
    }

    private void selfDestroy(){
        Destroy(gameObject);
    }
}
