using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static bool pauseMenu = false; //false = FPS, true = Menu
    [SerializeField] public GameObject pauseMenuUi;
    public static bool inDialogue = false; //false = FPS, true = in Dialogue
    [SerializeField] public GameObject dialogueMenuUi;

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
        } else {
            dialogueMenuUi.SetActive(false);
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
}
