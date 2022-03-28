using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreBoard : MonoBehaviour
{

    public static ScoreBoard Instance;

    public int score;

    //You need a kill every 4 seconds to get a multikill
    public float multiKillTimer;    //how much time has passed since last kill
    public float multiKillReset;    //how fast you must get the next kill
    public int multiKillTotal;      //number of kills in the multikill
    public bool active;             //is the multikill actively happening

    //How many points for each enemy
    public int titanPoints;         //
    public int lintPoints;          // Points for kills
    public int angelPoints;         //

    //total kills of each type
    public int totalTitans;         //
    public int totalLints;          //
    public int totalAngels;         // Total Kills
    public int totalKills;          //

    //Sprites + Images
    public Image doubleKill;
    public Image tripleKill;
    public Image quadKill;
    public Image fiveKill;
    public Image sixKill;
    public Image sevenKill;
    public Image eightKill;
    public Image nineKill;
    public Image tenKill;
    public float moveSpeed;
    public float xPos;

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

    //timers and kill count
    void FixedUpdate()
    {
        if (multiKillTimer >= multiKillReset)   
            multiKillEnd();                     //if mk reaches time limit end it
        else
            multiKillTimer += Time.deltaTime;   //else add time

        totalKills = totalTitans + totalLints + totalAngels;
    }

    //add kill and check if multikill
    public void addKill()
    {
        if (multiKillTotal >= 10)   //reset mk to 0 if it hits 10
            multiKillTotal = 0;

        multiKillTimer = 0;         //reset timer on kill
        multiKillTotal++;           //add kill
        multiKillActive();          //set mk to active
        CheckMultiKill();
    }

    //reference from titan script
    public void TitanKill()
    {
        score += titanPoints;
        totalTitans++;
        addKill();
    }

    //reference from lint script
    public void LintKill()
    {
        score += lintPoints;
        totalLints++;
        addKill();

    }

    //reference from angel script
    public void AngelKill()
    {
        score += angelPoints;
        totalLints++;
        addKill();

    }

    //activate multikill
    public void multiKillActive()
    {
        active = true;
    }

    //end multikill
    public void multiKillEnd()
    {
        multiKillTotal = 0;
        active = false;
    }

    //check for multikill, if yes determine which multikill
    public void CheckMultiKill()
    {
        if (active && multiKillTimer <= multiKillReset)
        {
            switch (multiKillTotal)
            {
                case 2:
                    Debug.Log("DOUBLE KILL\n");
                    doubleKill.enabled = true;
                    break;
                case 3:
                    Debug.Log("TRIPLE KILL\n");
                    tripleKill.enabled = true;
                    break;
                case 4:
                    Debug.Log("SQUAD WIPE\n");
                    quadKill.enabled = true;
                    break;
                case 5:
                    Debug.Log("DEMON TIME\n");
                    fiveKill.enabled = true;
                    break;
                case 6:
                    Debug.Log("6 PIECE\n");
                    sixKill.enabled = true;
                    break;
                case 7:
                    Debug.Log("CHAT CLIP THAT\n");
                    sevenKill.enabled = true;
                    break;
                case 8:
                    Debug.Log("MOM GET THE CAMERA\n");
                    eightKill.enabled = true;
                    break;
                case 9:
                    Debug.Log("ANOTHER ONE BITES THE DUST\n");
                    nineKill.enabled = true;
                    break;
                case 10:
                    Debug.Log("OUT OF MEDALS\n");
                    tenKill.enabled = true;
                    break;
                default:
                    Debug.Log("either 1 kill or more than 10 kills or error");
                    doubleKill.enabled = false;
                    tenKill.enabled = false;
                    break;
            }
        }
        else
            multiKillEnd();
    }

   
    //Displays the medals at the top middle part of the screen
     void medalDisplay()
    {
        /*
        have the medals scroll across the top of the screen from right to left
        maximum three medals on screen at a time
        medals wait a few seconds before sliding off naturally
        medals will be pushed off if the player gets a new medal  
       
         * 
         
         
         */

    }
      
    
}
