using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Countdown : MonoBehaviour
{
  
    public static Text timerText;  //get countdown text from Unity
    public static bool CountdownComplete = false; //set countdown to incomplete status
    int TimerIndex = 3; //begin from number 3

	void Start ()
    {
        timerText = GetComponent<Text>();  //this script is attatched to the text gameObject
        InvokeRepeating("Timer", 1, 1);   //begin countdown
     
    }
	
    public void Timer() //timer method that counts down from 3 up to the "DESCEND" s
    {
        timerText.text = TimerIndex.ToString();
        TimerIndex -= 1;
        if (TimerIndex == -1)
        {
            timerText.text = "DESCEND!";
            CountdownComplete = true; //countdown complete, player starts moving in PlayerMovement class
           
        }
        else if (TimerIndex == -2)
        {
            timerText.enabled = false;
            CancelInvoke("Timer"); //stop timer
        }
      
    }

}
