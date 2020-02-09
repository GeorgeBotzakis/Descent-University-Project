using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    private static int i = 1; //times that a new prefab will generate 
                              //smart way to accurately instantiate on the correct Y position, each time.
    public Transform OriginalPos; //original position of prefab
    public Transform prefab;   //the prefab of the level (background and walls)
    private  GameObject PrevChunk;


    
    void Start()
    {
        PrevChunk = prefab.gameObject; //get the previous chunk of map
    }

    private void OnTriggerEnter(Collider other) //when the player enters this collider, start the process of 
    {                                           //generating the next chunk of the level
        if (other.tag == "Player") 
        {
            Instantiate(prefab, new Vector3(OriginalPos.transform.position.x, 
                OriginalPos.position.y - 82.28f * i, OriginalPos.transform.position.z), Quaternion.identity);

            for (int j = 0; j <SpikeGeneration.SpikeArray.Length; j++)
            {               
                SpikeGeneration.SpikeArray[j].SetActive(true); //in case any previous items were disabled, re-enable them for the next chunk
                if (j < SpikeGeneration.LongSpArray.Length)
                    SpikeGeneration.LongSpArray[j].SetActive(true);

                Instantiate(SpikeGeneration.SpikeArray[j], new Vector3(Random.Range(-4.551f, 4.504f),
                    SpikeGeneration.SpikeArray[j].transform.position.y - 82.28f * i,0f), Quaternion.identity);

                if(j<SpikeGeneration.LongSpArray.Length)
                Instantiate(SpikeGeneration.LongSpArray[j], new Vector3(Random.Range(-4.55f,1.41f), 
                    SpikeGeneration.LongSpArray[j].transform.position.y - 82.28f * i, 0f), Quaternion.identity);
            }
            for(int z=0; z < CoinGeneration.CoinArray.Length;z++)
            {
                CoinGeneration.CoinArray[z].SetActive(true);
                Instantiate(CoinGeneration.CoinArray[z], new Vector3(Random.Range(-5.524f,5.424f),
                    CoinGeneration.CoinArray[z].transform.position.y - 82.28f * i, 0f),
                    Quaternion.Euler(90f,0f,0f));
                
            }
            //Shield powerup generation for each chunk generation
            ShieldGenerator.ShieldPrefab.SetActive(true);
            if(i%2==0) //every 2 chunks, spawn a shield powerup
            Instantiate(ShieldGenerator.ShieldPrefab, new Vector3(ShieldGenerator.Sh_positions[Random.Range(0, 2)], 
                ShieldGenerator.ShieldPrefab.transform.position.y - 82.28f * i,0f), Quaternion.identity);

            i++;  //variable incerements to indicate each level chunk passed 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(PrevChunk);   //destroy previous chunk of the map when the player exits the collider  
        }
    }
 
}
