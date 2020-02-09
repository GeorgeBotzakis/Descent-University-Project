using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    //767676FF def
    //FFFF00FF yellow color of first bar
    public Image[] HeatBars = new Image[5];
    Color EmptyBarColour = new Color32(118, 118, 118, 255);  //gray
    Color[] DefaultColours = new Color[5];
    int i; //array index
    public static Text CooldownText;
    public static Text CurrentScoreText;
    public static Text HighScoreText;
    public static Text DamageText;
    private static GameObject PausePanel;
    void Start ()
    {
        PausePanel = GameObject.FindGameObjectWithTag("PauseTXT");
        CooldownText = GameObject.FindGameObjectWithTag("CDText").GetComponent<Text>();
        CurrentScoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        HighScoreText = GameObject.FindGameObjectWithTag("HighScoreTXT").GetComponent<Text>();
        DamageText = GameObject.FindGameObjectWithTag("DMGText").GetComponent<Text>();

        DamageText.color = Color.green; //reset damage points text colour to green
        ScoreUpdater(0); //reset current score
        PausePanel.SetActive(false); //at the start of the game, do not show the pause panel to the player UI

        for (i = 0; i <= 4; i++)
        {
            DefaultColours[i] = HeatBars[i].color;
            HeatBars[i].color = EmptyBarColour;
        }
      
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (PlayerMovement.HeatIncreaseCheck) //check if player is wallriding to begin heat bar colouring sequence
        {                      
           ApplyHeatColours();               
        }
        else
        {         
            ResetColours(); //reset heat bar colours
        }

    }

    public IEnumerator HeatBarIncrease()
    {
      //apply each bar colour one bar each second
        yield return new WaitForSeconds(1f);
        HeatBars[0].color = DefaultColours[0];
        yield return new WaitForSeconds(1f);
        HeatBars[1].color = DefaultColours[1];
        yield return new WaitForSeconds(1f);
        HeatBars[2].color = DefaultColours[2];
        yield return new WaitForSeconds(1f);
        HeatBars[3].color = DefaultColours[3];
        yield return new WaitForSeconds(1f);
        HeatBars[4].color = DefaultColours[4];
        
    }
   
    void ResetColours() //reset heatbar colours.
    {
        int i;
               
        StopCoroutine("HeatBarIncrease");
        for (i = 0; i <= 4; i++)
        {
            HeatBars[i].color = EmptyBarColour;
        }
    }
    void ApplyHeatColours() //a method that starts the coroutine in Update().
    {
        StartCoroutine("HeatBarIncrease");
    }

    public static void ScoreUpdater(int score) //update score text via parameter
    {
        CurrentScoreText.text = "Score: " + score.ToString();
    }  
    public static void DamageUpdater(int dmg)  //method that updates damage text based on an int input
    { 
        if (dmg>=2 && dmg<=3)
        {
            DamageText.color = Color.yellow;
        }
        else if (dmg>=4)
        {
            DamageText.color = Color.red;
        }
        DamageText.text = "Damage Points: " + dmg.ToString() + "/5";
    }
    public static void PausePanelControls(bool Switch )  //control visibility of pause menu
    {
       
        if (Switch == true)
            PausePanel.SetActive(true);
        else
            PausePanel.SetActive(false);
    }
 
}
