using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{
    static MeshRenderer ShieldSphere;
	
	void Start ()
    {
        ShieldSphere = GameObject.FindGameObjectWithTag("Shield").GetComponent<MeshRenderer>();
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ShieldEngaged(); 
            this.gameObject.SetActive(false); //disable shield powerup
        }
    }

    private static void ShieldEngaged() //protect the player from one point of damage or from one spike trap
    {
        PlayerStates.isShielded = true;
        ShieldSphere.enabled = true;
    }
    public static void ShieldDestroyed() //disable shield after its use
    {
        PlayerStates.isShielded = false;
        ShieldSphere.enabled = false;
    }
}
