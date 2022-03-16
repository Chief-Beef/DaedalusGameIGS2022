using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{

    public static ScoreBoard Instance;

    public int score;

    //You need a kill every 4 seconds to get a multikill
    public float multiKillTimer;    //how much time has passed since last kill
    public float multiKillReset;    //how fast you must get the next kill
    public int multiKillTotal;      //number of kills in the multikill
    public bool active;    //is the multikill actively happening

    //How many points for each enemy
    public int titanPoints;         //
    public int lintPoints;          // Points for kills
    public int angelPoints;         //

    //total kills of each type
    public int totalTitans;         //
    public int totalLints;          //
    public int totalAngels;         // Total Kills
    public int totalKills;          //

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        score = 0;
        totalTitans = 0;
        totalLints = 0;
        totalAngels = 0;
        totalKills = 0;

        active = false;
        multiKillTotal = 0;
    }

    void FixedUpdate()
    {
        if (multiKillTimer >= multiKillReset)   
            multiKillEnd();                     //if mk reaches time limit end it
        else
            multiKillTimer += Time.deltaTime;   //else add time

        totalKills = totalTitans + totalLints + totalAngels;
    }

    public void addKill()
    {
        if (multiKillTotal >= 10)   //reset mk to 0 if it hits 10
            multiKillTotal = 0;

        multiKillTimer = 0;         //reset timer on kill
        multiKillTotal++;           //add kill
        multiKillActive();          //set mk to active
        CheckMultiKill();
    }

    public void TitanKill()
    {
        score += titanPoints;
        totalTitans++;
        addKill();
    }

    public void LintKill()
    {
        score += lintPoints;
        totalLints++;
        addKill();

    }
    public void AngelKill()
    {
        score += angelPoints;
        totalLints++;
        addKill();

    }

    public void multiKillActive()
    {
        active = true;
    }

    public void multiKillEnd()
    {
        multiKillTotal = 0;
        active = false;
    }

    public void CheckMultiKill()
    {
        if (active && multiKillTimer <= multiKillReset)
        {
            switch (multiKillTotal)
            {
                case 2:
                    Debug.Log("DOUBLE KILL\n");
                    break;
                case 3:
                    Debug.Log("TRIPLE KILL\n");
                    Debug.Log(multiKillTotal);
                    break;
                case 4:
                    Debug.Log("OVERKILL\n");
                    break;
                case 5:
                    Debug.Log("KILLTACULAR\n");
                    break;
                case 6:
                    Debug.Log("KILLTROCITY\n");
                    break;
                case 7:
                    Debug.Log("KILLIMANJARO\n");
                    break;
                case 8:
                    Debug.Log("KILLTASTROPHE\n");
                    break;
                case 9:
                    Debug.Log("KILLPOCALYPSE\n");
                    break;
                case 10:
                    Debug.Log("KILLIONAIRE -- MOM GET THE CAMERA\n");
                    break;
                default:
                    Debug.Log("either 1 kill or more than 10 kills or error");
                    break;

            }
        }
        else
            multiKillEnd();
    }

}
