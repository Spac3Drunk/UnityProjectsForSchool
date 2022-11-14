using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPS_GUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI hpHolder;
    [SerializeField] private TextMeshProUGUI ammoHolder;
    [SerializeField] private TextMeshProUGUI magHolder;
    [SerializeField] private TextMeshProUGUI scoreHolder;
    [SerializeField] private TextMeshProUGUI scoreMultHolder;

    void FixedUpdate(){
        hpHolder.text = "HP : " + ((int)(GameState.hp+0.8)).ToString();
        ammoHolder.text = "Ammo Stock : " + GameState.ammoInStock.ToString();
        magHolder.text = Weapon.bulletsLeft.ToString();
        scoreHolder.text = "Score : " + GameState.score.ToString();
        scoreMultHolder.text = "Combo Multiplier x" + GameState.scoreComboMult.ToString();
    }
}
