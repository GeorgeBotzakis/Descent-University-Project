using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuScripts : MonoBehaviour
{
    #region GameGuide
    public void GoToGameGuide()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));  //go from menu to game guide
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex -1));  //go from game guide to main menu
    }
    #endregion

    #region BeginGame

    public void BeginGame()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 2));
    }
    #endregion

    #region In-Game
    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
    public void RestartGame()
    {
        
  
        Countdown.CountdownComplete = false;   //prevent player from immediately moving
        Time.timeScale = 1;   //set game timescale to 1 to prevent pause override
        SceneManager.LoadScene(2);
        
    }
    public void ResumeGame()
    {
        PlayerStates.ResumeGame();
    }

    public static void GameOver()
    {
        SceneManager.LoadScene(3); //load game over screen
    }
    #endregion

    #region GameOver
    public void LoadGameFromGO()
    {
        
        Countdown.CountdownComplete = false; //make sure the countdown commences
        Time.timeScale = 1; //unpause game to prevent any overrides


        SceneManager.LoadScene(2);
    }
    #endregion

    #region AppExit
    public void CloseApp()
    {
        Application.Quit(); //close app only works in Build
    }
    #endregion
}
