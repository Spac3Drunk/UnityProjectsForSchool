using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] public GameObject FPS_Ui;
    public static bool pauseMenu = false; //false = FPS, true = Menu
    [SerializeField] public GameObject pauseMenuUi;
    public static bool inDialogue = false; //false = FPS, true = in Dialogue
    [SerializeField] public GameObject dialogueMenuUi;
    public static bool dead = false; //false = alive, true = Dead     player
    [SerializeField] public GameObject DeadScreenUi;

    public static float hp = 100;
    public static int score = 0;
    public static float scoreComboMult = 1;
    private static float comboTimer = 0.0f;

    public static int ammoInStock = 100;

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu) {
            Time.timeScale = 0f;
            pauseMenuUi.SetActive(true);
        } else {
            Time.timeScale = 1f;
            pauseMenuUi.SetActive(false);
        }

        if (inDialogue) {
            dialogueMenuUi.SetActive(true);
            FPS_Ui.SetActive(false);
        } else {
            dialogueMenuUi.SetActive(false);
            FPS_Ui.SetActive(true);
        }

        if (comboTimer > 0) {
            comboTimer -= Time.deltaTime;
        }
        if (comboTimer <= 0) {
            comboTimer = 0;
            scoreComboMult = 1;
        }


        if (hp <= 0) {
            dead = true;
        } else {
            DeadScreenUi.SetActive(false);
        }

        if (dead) {
            Time.timeScale = 0f;
            DeadScreenUi.SetActive(true);
            pauseMenuUi.SetActive(false);
            dialogueMenuUi.SetActive(false);
            FPS_Ui.SetActive(false);
        }
    }

    public static void togglePause()
    {
        pauseMenu = !pauseMenu;
    }

    public static void dialogue_On()
    {
        inDialogue = true;
    }
    public static void dialogue_Off()
    {
        inDialogue = false;
    }

    public static void markScore()
    {
        score += (int)(100 * scoreComboMult);
        scoreComboMult += 0.5f;
        comboTimer = 5;
    }

    public static void getHit()
    {
        hp -= 0.2f;
    }
    public static void getHeal(int healAmount)
    {
        hp += healAmount;
        if (hp > 100)
        {
            hp = 100;
        }
    }

    public static int useAmmoStock(int nbAmmoRequsted, int ammoConsumption)
    {
        if (nbAmmoRequsted*ammoConsumption > ammoInStock){
            int tmp = (int)(ammoInStock/ammoConsumption);
            ammoInStock -= ammoConsumption*tmp;
            return tmp;
        } else {
            ammoInStock -= nbAmmoRequsted*ammoConsumption;
            return nbAmmoRequsted;
        }
    }
    public static void fillAmmoStock(int ammoFilled)
    {
        ammoInStock += ammoFilled;
        if (ammoInStock > 200)
        {
            ammoInStock = 200;
        }
    }
}
