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

            if (species == "Lint")
            {
                transform.parent.GetComponent<SwarmScript>().Fart();

                if (survival)
                    GameObject.FindGameObjectWithTag("KillCounter").GetComponent<Titan_Spawner>().LintKill();
            }
            else if (species == "Flying One")
            {
                transform.parent.GetComponent<TheFlyingOne>().InstantiateDeathEffect();

                if (survival)
                    GameObject.FindGameObjectWithTag("KillCounter").GetComponent<Titan_Spawner>().FlyingKill();
            }

            Destroy(this.transform.parent.gameObject);
        }
    }
}
