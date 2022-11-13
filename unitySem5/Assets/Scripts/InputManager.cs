using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputs playerInputs;
    private PlayerInputs.OnFootActions onFoot;
    private PlayerInputs.UiRelatedActions uiRelated;
    private PlayerInputs.DialogueRelatedActions dialogueRelated;

    private PlayerMotor motor;
    private PlayerLook look;
    private Weapon weapon;

    // Start is called before the first frame update
    void Awake()
    {
        playerInputs = new PlayerInputs();

        onFoot = playerInputs.OnFoot;
        uiRelated = playerInputs.UiRelated;
        dialogueRelated = playerInputs.DialogueRelated;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        weapon = GetComponent<Weapon>();

        //inputs
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.WeaponFire.performed += ctx => weapon.useWeapon();
        onFoot.WeaponReload.performed += ctx => weapon.Reload();

        look.MouseLockAndInvisible(GameState.pauseMenu);

        uiRelated.Echap.performed += ctx => {
            GameState.togglePause();
            look.MouseLockAndInvisible(GameState.pauseMenu);
            if (GameState.pauseMenu) {
                onFoot.Disable();
            }else {
                onFoot.Enable();
            }
        };

    }

    void FixedUpdate(){
            motor.ProcessMove(onFoot.Movements.ReadValue<Vector2>());
    }

    private void LateUpdate(){
            look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable(){
        onFoot.Enable();
        uiRelated.Enable();
    }
    private void OnDisable(){
        onFoot.Disable();
        uiRelated.Disable();
    }

}
