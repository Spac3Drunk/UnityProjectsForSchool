using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;

    public float speed = 12f;
    public float gravity = -10f;
    public float jumpHeight = 2f;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = controller.isGrounded;
    }

    public void ProcessMove(Vector2 input) {
        Vector3 movements = Vector3.zero;
        movements.x = input.x;
        movements.z = input.y;
        controller.Move(transform.TransformDirection(movements)*speed*Time.deltaTime);
        playerVelocity.y += gravity*Time.deltaTime;
        if ((isGrounded) && playerVelocity.y < 0){
            playerVelocity.y = -0.1f;
        }
        controller.Move(playerVelocity*Time.deltaTime);
    }

    public void Jump() {
        if (isGrounded) {
            playerVelocity.y = jumpHeight*gravity*-0.33f;
        }
    }
}
