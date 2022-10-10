using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputs playerInputs;
    private PlayerInputs.OnFootActions onFoot;

    private PlayerMotor motor;
    private PlayerLook look;
    private Weapon weapon;

    // Start is called before the first frame update
    void Awake()
    {
        playerInputs = new PlayerInputs();
        onFoot = playerInputs.OnFoot;
        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        weapon = GetComponent<Weapon>();
        //inputs
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.WeaponFire.performed += ctx => weapon.useWeapon();
        onFoot.WeaponReload.performed += ctx => weapon.Reload();
    }

    void FixedUpdate(){
        motor.ProcessMove(onFoot.Movements.ReadValue<Vector2>());
    }

    private void LateUpdate(){
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable(){
        onFoot.Enable();
    }

    private void OnDisable(){
        onFoot.Disable();
    }

}
