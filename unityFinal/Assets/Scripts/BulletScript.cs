using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //TODO bullet that can bounce

    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask badGuys;

    public float explosionDmg;
    public float explosionRange;
    public float explosionForce;
    public AudioClip clip;

    public float maxLifetime;

    private void FixedUpdate(){
        maxLifetime -= Time.deltaTime;
        if (maxLifetime <= 0) Invoke("selfDestroy", 0.05f);
    }

    private void OnCollisionEnter(Collision collision){
            Explode();
    }
    
    private void Explode(){
        if (explosion != null){
            Instantiate(explosion, transform.position, Quaternion.identity);
            SoundManager.instance.PlaySound(clip);
        }
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, badGuys);
        for (int i = 0; i < enemies.Length; i++)
        {   
            //Debug.Log(enemies[i]);
            enemies[i].GetComponent<BadGuys>().takeDmg(explosionDmg);

            //Add explosion force (if enemy has a rigidbody)
            if (enemies[i].GetComponent<Rigidbody>()){
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);
            }
        }

        //Add a little delay, just to make sure everything works fine
        Invoke("selfDestroy", 0.05f);
    }
    private void selfDestroy(){
        Destroy(gameObject);
    }

    //debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }
}
