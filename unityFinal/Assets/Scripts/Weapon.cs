using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //TODO recoil, muzzle flash, Parallax
    
    public GameObject bullet;

    public static int bulletsLeft;

    public float shootForce;
    public float spread, reloadTime, timeBetweenShooting;
    public int magazineSize, bulletsPerTap, ammoConsumption;

    private int bulletsShot;
    private bool readyToShoot, reloading;

    public Camera fpsCam;
    public Transform attackPoint; //spawnpoint des bullets

    public bool allowInvoke = true; //bug fixing :D


    private void Awake()
    {
        //start with magazine full
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    public void useWeapon(){
        if (readyToShoot && !reloading && bulletsLeft >= ammoConsumption) {
            bulletsLeft -= ammoConsumption;
            bulletsShot = 0; // useful for shotgun
            Shoot();
        }
    }

    private void Shoot()
    {
        readyToShoot = false;

        //create a point 100unit in front of the center of camera
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Vector3 targetPoint = ray.GetPoint(100);

        //Calculate direction from attackPoint to targetPoint
        Vector3 directionWithoutSpread = targetPoint - attackPoint.position;

        //Calculate spread
        float x;
        float y;
        do{
            x = Random.Range(-spread, spread);
            y = Random.Range(-spread, spread);
        } while (x*x+y*y > spread);
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);

        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity);
        currentBullet.transform.forward = directionWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);

        bulletsShot++;

        //Invoke resetShot function (if not already invoked), with your timeBetweenShooting
        if (allowInvoke) {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }

        //repeat shoot function for shotgun
        if ((bulletsPerTap - bulletsShot) > 0 ) {
            Shoot();
        }
    }

    private void ResetShot()
    {
        //Allow shooting and invoking again
        readyToShoot = true;
        allowInvoke = true;
    }

    public void Reload()
    {
        reloading = true;
        Invoke("ReloadFinished", reloadTime); //Invoke ReloadFinished function with your reloadTime as delay
    }
    private void ReloadFinished()
    {
        //Fill magazine
        bulletsLeft += GameState.useAmmoStock((magazineSize - bulletsLeft), ammoConsumption);
        reloading = false;
    }
}
