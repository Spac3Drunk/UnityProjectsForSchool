using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static bool pauseMenu = false; //false = FPS, true = Menu
    [SerializeField] public GameObject pauseMenuUi;

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
    }

    public static void togglePause()
    {
        pauseMenu = !pauseMenu;
    }
}
