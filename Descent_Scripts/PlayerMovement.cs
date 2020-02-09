using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    CharacterController controller;

    Vector2 movement;
   

    float movementSpeed = 20f;
    float gravity = 9.81f;      //default:9.81
    float ForceUpward = 4.0f;   //default:4.0
  
    static bool WallRiding = false;
    static bool HeatedUp = false;
    static bool RightRide = false;
    static bool LeftRide = false;
    public static bool HeatIncreaseCheck = false;
   

    float HeatPeriod = 5.0f;
    float CoolPeriod = 6.0f;
    float DamageInterval = 1.2f;

    float timestamp;  //heat timestamp
    float timestamp2; //cooldown timestamp
    float timestamp3; //heat damage timestamp
   
    int timesrun = 0;  //counters that prevent the instantaneous use of the Heat-related methods and status changes.
    int timesrun2 = 0; // basically, preventing instant heat or cooldown by waiting for the next cooldown.
    int timesrun3 = 0;
    static int DMGpoints; // damage points the player receives, used to be copied into another method in another class

    int i = 0;  //number of loops in the MoveOverTime function

    
    void Start ()
    {
        controller = GetComponent<CharacterController>();
        DMGpoints = PlayerStates.PlayerDamagePoints;
    }
 
  
	void Update ()
    {
        
        if (Countdown.CountdownComplete == true) //when countdown completes begin the Game Loop
        {

            movement = new Vector2(movementSpeed * Input.GetAxis("Horizontal"), 0); //player controlled movement limited to H-axis
           
          
            if (WallRiding) 
            {             
                movement.y -= gravity - ForceUpward; //force added to counter gravity variable
                HeatIncreaseCheck = true;       //begin heat bear sequence             
                if (Time.time > timestamp)
                {
                    HeatState();
                }
              
                if (HeatedUp)
                {
                    if (Time.time > timestamp3)
                    {
                        HeatDamage();                 
                    }                                  
                }
                
            }
            else
            {
                movement.y -= gravity; //player keeps descending with this variable which acts as gravity, without the use of rigidbody
             
                
                
                if (!WallRiding && !HeatedUp)  
                {
                    HeatIncreaseCheck = false;  //reset bar colours
                    timesrun = 0;    //reset wallriding heat when off wall             
                }
               
            }
          
            if(HeatedUp && !WallRiding)  //check if player has "heated up" and begin cooldown until next wallride
            {
                PlayerUI.CooldownText.enabled = true; //enable the "COOLING" text
             
                if (Time.time > timestamp2)
                {
                    CoolState();    //after 6 seconds pass, start cooldown 
                     
                }             

            }
            else
            {
                timesrun2 = 0; //cooldown will cancel if player hits wall again in "Heated" status
                PlayerUI.CooldownText.enabled = false; //turn off text for cooldown
            }

            controller.Move(movement * Time.deltaTime); //apply movement to the player 
        }
	}
    #region Player / Enviroment Relationship 
    private void OnTriggerEnter(Collider other)  //when the player starts wallriding, 
                                                 //which means to enter the second box collider of each wall
    {
        if (other.tag == "RightWall" || other.tag == "LeftWall") //check if player is wallriding ,no matter which wall
        {
            WallRiding = true;
            if (other.tag == "RightWall")
            {
                RightRide = true;
            }
            else if (other.tag == "LeftWall")
            {
                LeftRide = true;
            }
        }

        if (other.tag == "Spikes" || other.tag=="LongSpikes")   //when player enters spikes' trigger collider, trigger DeathState
        {
            if (PlayerStates.isShielded)
            {
                other.gameObject.SetActive(false); //disable spike so the player will continue descent

                ShieldPowerUp.ShieldDestroyed();
            }
            else
            PlayerStates.DeathState();
        }
    }
    private void OnTriggerExit(Collider other)   //when the player stops wallriding
                                                 //which means to exit the second box collider of each wall
    {
        if (other.tag == "RightWall" || other.tag == "LeftWall")
        {
            WallRiding = false;          
            if (other.tag == "RightWall")
            {
                RightRide = false;
            }
            else if (other.tag == "LeftWall")
            {
                LeftRide = false; 
            }
        }
    }

   private void CoolState() //cooldown state after player is HEATED UP and not wallriding. After 6 seconds, "cool" the player.
    {
        if (timesrun2 == 0 || timesrun2 == 1)     
            timesrun2++;
        
        if (timesrun2 > 1)
        {
            PlayerUI.CooldownText.enabled = false; //disable cooldown text
            HeatedUp = false; //stop the player from receiving damage the next time wallride is engaged
            HeatIncreaseCheck = false; //reset heatbar colours
            timesrun3 = 0; //reset heat damage method counter for next use
            i = 0; //reset the counter for MoveTowards method so it can be used again
        }


        timestamp2 = Time.time + CoolPeriod; //timestamp that adds the cooldown times to current time since the game started
    }
   private void HeatState() //after 5 seconds of non-stop wallriding, launch the player off and begin HEATED status.
    {
        if (timesrun == 0 || timesrun== 1)
            timesrun++;

        if (timesrun > 1)
        {
            HeatedUp = true;
            if (LeftRide) //based on which wall the player wallrides, launch in the correct direction
            {
               
                StartCoroutine(MoveOverSpeed(this.gameObject, new Vector3(this.transform.position.x+3.0f,this.transform.position.y,this.transform.position.z),8f));
            }
            
            if (RightRide) //based on which wall the player wallrides, launch in the correct direction
            {
                StartCoroutine(MoveOverSpeed(this.gameObject, new Vector3(this.transform.position.x - 3.0f, this.transform.position.y, this.transform.position.z), 8f));
            }                          
        }
      
        timestamp = Time.time + HeatPeriod;     
    }

    private void HeatDamage()   //apply damage points to player when he wallrides while heated up.
    {
        if (timesrun3 == 0 || timesrun3==1)
            timesrun3++;

        if (timesrun3 > 1)
        {
            if (PlayerStates.isShielded)
            {
                ShieldPowerUp.ShieldDestroyed(); //destroy shield after 1.2 seconds, preventing immediate damage 
            }
            else
            {
                DMGpoints++; //player is inflicted one damage per 1.2 seconds 
                PlayerStates.PlayerDamagePoints = DMGpoints;
                PlayerStates.DamageReceiver(DMGpoints); //method that accepts the damage value that the player received
            }           
        }
        timestamp3 = Time.time + DamageInterval;
    }
    public IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed)
    {

        //The MoveTowards method will finish after the number of loops has reached 18, 
        //I implemented this because it wouldn't finish even if it reached the desired end position,
        //due to the constant moving nature of the object, which would interfere with the gravity variable.

        while (objectToMove.transform.position != end && i!=18)  // I used an int of i to control the number of loops, 
                                                                 //so that the Lerping won't continue after completing a short distance.
        {
            objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
            i++;
            yield return new WaitForEndOfFrame();    
        }
      
    }
  
    #endregion   
}
