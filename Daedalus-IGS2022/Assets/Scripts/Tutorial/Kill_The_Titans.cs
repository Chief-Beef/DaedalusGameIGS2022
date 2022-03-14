using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill_The_Titans : MonoBehaviour
{
    public GameObject[] titans;
    public int remainingTitans;

    public void KillTitan()
    {
        remainingTitans--;
        if (remainingTitans <= 0)
        {
            Debug.Log("all titans are dead!");
        }
    }
}
