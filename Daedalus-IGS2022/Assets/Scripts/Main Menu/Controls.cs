using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject screen;

    public void Screen()
    {
        if (screen.activeInHierarchy)
            screen.SetActive(false);
        else
            screen.SetActive(true);
    }
}
