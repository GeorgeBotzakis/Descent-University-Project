using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInteractivity : MonoBehaviour
{
   static int CurrentScore;
   public  static bool isRotating=true;
	void Start ()
    {
        CurrentScore = PlayerStates.Score; //get current score from PlayerStates class       
	}
	
	
	void Update ()
    {       
        if (isRotating)
        {
            transform.Rotate(0, 0, 3); //rotation of the coin
        }           
    }
    private void OnTriggerEnter(Collider other)  //when coin's trigger detects another collider
    {
        if (other.tag == "Player")    //when the player collects a coin
        {
            this.gameObject.SetActive(false);   //destroy gameobject so it would appear as collected
            CoinCollected();
        }
    }

    public void CoinCollected()
    {
        CurrentScore+=10; //Update current score

        PlayerStates.Score = CurrentScore;  //update the public static Score within the PlayerStates Script
        PlayerUI.ScoreUpdater(CurrentScore); //update UI 
    
    }
}
