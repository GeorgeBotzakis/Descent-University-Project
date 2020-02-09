using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerator : MonoBehaviour
{
    public static float[] Sh_positions = new float[3] { -5.16f, 0f, -5.16f }; // 3 desired X-axis positions for the shield powerup
    public static GameObject ShieldPrefab;

    void Start() //get shield prefab for use in the next chunks of the level
    {
        ShieldPrefab = GameObject.FindGameObjectWithTag("ShieldPowerUp");
	}
}
