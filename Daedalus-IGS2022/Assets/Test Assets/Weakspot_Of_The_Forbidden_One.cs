using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weakspot_Of_The_Forbidden_One : MonoBehaviour
{
    public string species;
    public bool survival;

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "PlayerAttack")
        {
            ScoreBoard.Instance.kill(species);

            if (survival)
            {
                if (species == "Lint")
                    GameObject.FindGameObjectWithTag("KillCounter").GetComponent<Titan_Spawner>().SpawnLint();
                else if (species == "Flying One")
                    GameObject.FindGameObjectWithTag("KillCounter").GetComponent<Titan_Spawner>().SpawnFlying();
            }

            Destroy(this.transform.parent);
        }
    }
}
