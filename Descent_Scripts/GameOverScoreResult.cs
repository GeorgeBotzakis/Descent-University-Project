using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScoreResult : MonoBehaviour
{
    public static Text ResultTXT;

    void Start() //Based on player score result, show appropriate response
    {
        ResultTXT = GameObject.FindGameObjectWithTag("ResultTXT").GetComponent<Text>();
        if (PlayerStates.LastScore > PlayerStates.HighScore)
        {
            ResultTXT.text = "NEW HIGH SCORE: " + PlayerStates.LastScore.ToString();
        }
        else
        {
            ResultTXT.text = "\n You achieved a score of " + PlayerStates.LastScore.ToString();
        }

    }
}
