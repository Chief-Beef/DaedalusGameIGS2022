using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakspot_Of_The_Forbidden_One : MonoBehaviour
{
    public GameObject parentObject;
    public string species;
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "PlayerAttack")
        {
            ScoreBoard.Instance.kill(species);
            Destroy(parentObject.gameObject);
        }
    }
}
