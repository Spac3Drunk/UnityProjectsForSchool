using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public GameObject fpsCam;
    private float rot = 0f;

    public float xSensi = 50f;
    public float ySensi = 50f;

    public void ProcessLook(Vector2 input){
        float mouseX = input.x;
        float mouseY = input.y;
        //calculate the rotation up down
        rot -= mouseY * Time.deltaTime * ySensi;
        rot = Mathf.Clamp(rot, -90f, 90f);
        fpsCam.transform.localRotation = Quaternion.Euler(rot, 0, 0);
        //left right
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensi);
    }

    public void MouseLockAndInvisible(bool uiState){
        if(uiState){
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
