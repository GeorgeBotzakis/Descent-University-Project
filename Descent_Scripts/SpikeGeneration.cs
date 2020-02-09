using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeGeneration : MonoBehaviour
{
    public static GameObject[] SpikeArray;
    public static GameObject[] LongSpArray;
   
 

   // initialise array of spikes and longspikes in the first chunk of level for them to be cloned
    void Start ()
    {
        SpikeArray = GameObject.FindGameObjectsWithTag("Spikes");
        LongSpArray = GameObject.FindGameObjectsWithTag("LongSpikes");                         
    }
}
