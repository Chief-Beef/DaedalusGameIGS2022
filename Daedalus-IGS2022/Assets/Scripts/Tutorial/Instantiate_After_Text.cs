using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate_After_Text : MonoBehaviour
{
    public Text_Sequencer ts;
    private bool spawned = false;
    public GameObject spawnObject;
    public Vector3 spawnPos;
    public GameObject[] objectToEnable;

    private void Update()
    {
        // Will spawn and enable objects once text is done scrolling
        if (!spawned && ts.noMoreMessages)
        {
            if (spawnObject != null)
            {
                Instantiate(spawnObject, spawnPos, Quaternion.identity, null);
            }
            if (objectToEnable != null)
            {
                for (int i = 0; i < objectToEnable.Length; i++)
                {
                    objectToEnable[i].SetActive(true);
                }
            }
            spawned = true;
            Destroy(this.gameObject);
        }
    }
}
