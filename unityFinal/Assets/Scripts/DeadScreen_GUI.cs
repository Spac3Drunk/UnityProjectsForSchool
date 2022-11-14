using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeadScreen_GUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreHolder;

    void Update(){
        scoreHolder.text = " Your Score : " + GameState.score.ToString();
    }
}
