using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPickup : MonoBehaviour
{
    private bool hasBeenUsed = false; //debug :D
    public GameObject playerObject;
    [SerializeField] private Transform targetPos;
    public float lifetime = 30;

    void Awake(){
        playerObject = GameObject.FindGameObjectsWithTag("Player")[0];
        targetPos = playerObject.transform;
    }

    void FixedUpdate(){
        if (Vector3.Distance(this.transform.position,targetPos.position) < 1)
        {
            if (!hasBeenUsed)
            {
                GameState.getHeal(10);
                hasBeenUsed = true;
            }
            Invoke("selfDestroy", 0.05f);
        }
        if (lifetime < 0)
        {
            Invoke("selfDestroy", 0.05f);
        }
        lifetime -= Time.deltaTime;
    }

    private void selfDestroy(){
        Destroy(gameObject);
    }
}
