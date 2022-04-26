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
                {
                    GameObject.FindGameObjectWithTag("KillCounter").GetComponent<Titan_Spawner>().LintKill();
                }
                else if (species == "Flying One")
                    GameObject.FindGameObjectWithTag("KillCounter").GetComponent<Titan_Spawner>().FlyingKill();
            }
            else
            {
                if (species == "Lint")
                {
                    transform.parent.GetComponent<SwarmScript>().Fart();
                }
                else if (species == "Flying One")
                    transform.parent.GetComponent<TheFlyingOne>().InstantiateDeathEffect();
            }

            Destroy(this.transform.parent.gameObject);
        }
    }
}
