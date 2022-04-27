using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScoreBoard : MonoBehaviour
{

    public static ScoreBoard Instance;

    public int score;
    public Rigidbody2D rb;

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
    public Image[] medalWheel = new Image[3];
    public Image image1, image2, image3;
    public Text[] txtWheel = new Text[3];
    public Text txt1, txt2, txt3;

    public Sprite doubleKill;
    public Sprite tripleKill;
    public Sprite quadKill;
    public Sprite fiveKill;
    public Sprite sixKill;
    public Sprite sevenKill;
    public Sprite eightKill;
    public Sprite nineKill;
    public Sprite tenKill;

    public Sprite fiveSpree;
    public Sprite tenSpree;
    public Sprite fifteenSpree;
    public Sprite twentySpree;
    public Sprite thirtySpree;

    public Sprite rocketMan;
    public Sprite fireball;
    public Sprite lightningBolt;

    private Vector3 midPoint, leftPoint, rightPoint;
    private string mkText;

    public int medalCounter;
    public int killStreak;


    //Speed Checks
    public float xSpeed, ySpeed, linearSpeed;


    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        midPoint = image1.transform.position;
        leftPoint = new Vector3(midPoint.x - 140, midPoint.y, midPoint.z);
        rightPoint = new Vector3(midPoint.x + 140, midPoint.y, midPoint.z);

        medalWheel[0].enabled = false;
        medalWheel[1].enabled = false;
        medalWheel[2].enabled = false;

        txtWheel[0].enabled = false;
        txtWheel[1].enabled = false;
        txtWheel[2].enabled = false;



        score = 0;
        totalTitans = 0;
        totalLints = 0;
        totalAngels = 0;
        totalKills = 0;

        active = false;
        multiKillTotal = 0;

        if (rb == null)
            rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
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
        killStreak++;
        multiKillActive();          //set mk to active
        CheckMultiKill();           //check mks
        CheckStreak();              //check killstreak
        playerSpeed();              //check player speed
    }

    //reference from titan script
    public void TitanKill()
    {
        score += titanPoints;
        totalTitans++;
        addKill();
        //NoisyBoi.Instance.MakeNoise(1);
    }

    //reference from lint script
    public void LintKill()
    {
        score += lintPoints;
        totalLints++;
        addKill();
        //NoisyBoi.Instance.MakeNoise(2);

    }

    //reference from angel script
    public void AngelKill()
    {
        score += angelPoints;
        totalLints++;
        addKill();
        //NoisyBoi.Instance.MakeNoise(3);
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

        medalCounter = 0;
        medalWheel[0].enabled = false;
        medalWheel[1].enabled = false;
        medalWheel[2].enabled = false;

        txtWheel[0].enabled = false;
        txtWheel[1].enabled = false;
        txtWheel[2].enabled = false;
    }

    //check for multikill, if yes determine which multikill
    public void CheckMultiKill()
    {
        if (active && multiKillTimer <= multiKillReset)
        {
            switch (multiKillTotal)
            {
                case 2:
                    mkText = "DOUBLE KILL\n";
                    medalDisplay(doubleKill);
                    break;
                case 3:
                    mkText = "TRIPLE KILL\n";
                    medalDisplay(tripleKill);
                    break;
                case 4:
                    mkText = "SQUAD WIPE\n";
                    medalDisplay(quadKill);
                    break;
                case 5:
                    mkText = "DEMON TIME\n";
                    medalDisplay(fiveKill);
                    break;
                case 6:
                    mkText = "6 PIECE\n";
                    medalDisplay(sixKill);
                    break;
                case 7:
                    mkText = "CHAT CLIP THAT\n";
                    medalDisplay(sevenKill);
                    break;
                case 8:
                    mkText = "MOM GET THE CAMERA\n";
                    medalDisplay(eightKill);
                    break;
                case 9:
                    mkText = "CIVIL WAR DOCTOR\n";
                    medalDisplay(nineKill);
                    break;
                case 10:
                    mkText = "OUT OF MEDALS\n";
                    medalDisplay(tenKill);
                    break;
                default:
                    mkText = "either 1 kill or more than 10 kills or error";
                    break;
            }
        }
        else
            multiKillEnd();
    }


    //Displays the medals at the top middle part of the screen
    void medalDisplay(Sprite medal)
    {
        /*
        have the medals scroll across the top of the screen from right to left
        maximum three medals on screen at a time
        medals wait a few seconds before sliding off naturally
        medals will be pushed off if the player gets a new medal  
       
        three images in an array
        move images, when they get to x2 they are reset to x1
        change opacity or disable when they go offscreen to appear as if they are gone
         */

        //new medal enters at image 1
        medalWheel[medalCounter % 3].enabled = true;
        medalWheel[medalCounter % 3].sprite = medal;

        //new medal has text underneath
        txtWheel[medalCounter % 3].enabled = true;
        txtWheel[medalCounter % 3].text = mkText;

        moveMedals(medalCounter);

        medalCounter++; //next medal will be on the next image
    }

    //Moves the medals to show new medals on screen
    void moveMedals(int count)
    {

        if (count == 0)
            medalWheel[0].transform.position = midPoint;
        else if (count == 1)
            medalWheel[1].transform.position = rightPoint;
        else
        {
            //rotates medals
            medalWheel[(count-2)%3].transform.position = leftPoint;
            medalWheel[(count-1)%3].transform.position = midPoint;
            medalWheel[(count)%3].transform.position = rightPoint;
        }
        
    }

    //Kill Streak function
    //Awards a medal for a 5, 10 ,15, 20 and 30 spree
    public void CheckStreak()
    {
        switch (killStreak)
        {
            case 5:
                mkText = "KILLING SPREE\n";
                medalDisplay(fiveSpree);
                break;
            case 10:
                mkText = "RAMPAGE\n";
                medalDisplay(tenSpree);
                break;
            case 15:
                mkText = "TERMINATOR\n";
                medalDisplay(fifteenSpree);
                break;
            case 20:
                mkText = "JOHN WICK\n";
                medalDisplay(twentySpree);
                break;
            case 30:
                mkText = "GET IN THE FRIDGE\n";
                medalDisplay(thirtySpree);
                break;
            default:
                break;
        }
    }

    //detecting kills on either Lint or the Flying/Forbidden One
    public void kill(string name)
    {
        if (name == "Lint")
            LintKill();
        else if (name == "Flying One")
            AngelKill();
    }

    //Awards medals for getting a kill at a high speed
    //the higher the speed the better the medal
    public void playerSpeed()
    {
        xSpeed = rb.velocity.x;
        ySpeed = rb.velocity.y;

        linearSpeed = Mathf.Sqrt(xSpeed*xSpeed + ySpeed*ySpeed);

        if(linearSpeed >= 80.0f)
        {
            mkText = "IT WAS ME BARRY\n";
            medalDisplay(lightningBolt);
        }
        else if (linearSpeed >= 70.0f)
        {
            mkText = "SPEED KILLS\n";
            medalDisplay(fireball);
        }
        else if (linearSpeed >= 60.0f)
        {
            mkText = "ROCKET MAN\n";
            medalDisplay(rocketMan);
        }

        //Debug.Log("Linear Speed: " + linearSpeed);
    }
}
