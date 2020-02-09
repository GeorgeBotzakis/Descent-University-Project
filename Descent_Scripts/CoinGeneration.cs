using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGeneration : MonoBehaviour
{
    public static GameObject[] CoinArray;

    void Start () //at start of game get all coins in sceene for cloning on the next chunks of the level
    {
        CoinArray = GameObject.FindGameObjectsWithTag("Coin");
    }	
}
