using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerStates : MonoBehaviour
{
    public static int PlayerDamagePoints; 
    public static int Score;
    public static int LastScore;
    public static bool isShielded;   
    private static bool Paused = false;
    string highScoreKey = "HighScore";
    public static  int HighScore = 0;
    void Start ()
    {
        LastScore = 0;
        Score = 0;
        HighScore = PlayerPrefs.GetInt(highScoreKey, 0);
        // PlayerUI.HighScoreUpdater(HighScore);
       
        PlayerDamagePoints = 0; //No Damage at first
        isShielded = false;
        
    }
	

	void Update ()
    {
        if (!Paused && Countdown.CountdownComplete)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ResumeGame();
            }
        }
           //Set the UI to match the new high score,in-game.
            
            PlayerUI.HighScoreText.text = "High Score: " + HighScore.ToString();
        if (Score > HighScore)
        {
            PlayerUI.HighScoreText.text = "High Score: " + Score.ToString();
        }
       
    }
    private void OnDisable() //when the game ends save current high score
    {
        if (Score > HighScore)
        {
            PlayerPrefs.SetInt(highScoreKey, Score);
            PlayerPrefs.Save();
        }
    }

    public static void PauseGame()
    {
        CoinInteractivity.isRotating = false; //pause coin rotation
        Paused = true;
        Time.timeScale = 0; 
        PlayerUI.PausePanelControls(true);
    }
    public static void ResumeGame()
    {
        CoinInteractivity.isRotating = true; //resume coin rotation
        Paused = false;
        Time.timeScale = 1;
        PlayerUI.PausePanelControls(false);
    }

    public static void DeathState()
    {
        LastScore = Score;
        Time.timeScale = 0;
        MenuScripts.GameOver();  //go to game over screen
    }
    public static void DamageReceiver(int dmg)
    {
        if (dmg == 5)
        {
            DeathState(); //when damage points reach 5, trigger DeathState
        }
        PlayerUI.DamageUpdater(dmg); //update UI damage counter
        
    }
}
