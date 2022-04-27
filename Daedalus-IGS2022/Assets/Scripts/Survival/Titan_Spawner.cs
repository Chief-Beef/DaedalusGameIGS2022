using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Titan_Spawner : MonoBehaviour
{
    // The starting number of enemies
    public int titanCount;
    public int lintCount;
    public int flyingCount;
    // Current number of titans
    private int titans;
    private int lints;
    private int flying;
    // How long it takes to increase the number of enemies
    public float titanIncreaseTime;
    public float lintIncreaseTime;
    public float flyingIncreaseTime;
    // Max number of enemies
    public int maxTitans;
    public int maxLints;
    public int maxFlying;

    // Spawn points
    public Transform spawnPointA;
    public Transform spawnPointB;

    // Prefabs of enemies that can spawn
    public GameObject titanEnemy;
    public GameObject lintEnemy;
    public GameObject flyingEnemy;

    // Keeps track of player's kills
    private int titanKills = 0;
    private int lintKills = 0;
    private int flyingKills = 0;

    // Text keeps track of kills
    public Text kills;
    public Text record;

    // Music stuff
    public AudioSource track1;
    public AudioSource track2;
    public AudioSource track3;

    // Timer to control music
    private int time = 0;

    // Music volume
    public float volume1;
    public float volume2;
    public float volume3;

    // Player
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        StartCoroutine(IncreaseTitans());
        StartCoroutine(IncreaseLints());
        StartCoroutine(IncreaseFlying());

        track1.volume = volume1;
        StartCoroutine(MusicTimer());

        UpdateText();
    }

    // Timer for music
    IEnumerator MusicTimer()
    {
        if (player.activeInHierarchy)
        {
            yield return new WaitForSeconds(30);

            time++;

            if (time == 3)
            {
                track1.volume = 0;
                track2.volume = volume2;
            }
            else if (time == 6)
            {
                track2.volume = 0;
                track3.volume = volume3;
            }

            StartCoroutine(MusicTimer());
        }
    }

    // These are called when something is killed
    public void TitanKill()
    {
        SpawnTitan();
        titanKills++;
        UpdateText();
    }
    public void LintKill()
    {
        SpawnLint();
        lintKills++;
        UpdateText();
    }
    public void FlyingKill()
    {
        SpawnFlying();
        flyingKills++;
        UpdateText();
    }
    // Updates kill text
    public void UpdateText()
    {
        kills.text = "Current Kills:\n       Titan Kills: " + titanKills.ToString() + "\nRanged Enemy Kills: " + lintKills.ToString() + "\nFlying Enemy Kills: " + flyingKills.ToString();

        if (titanKills > PlayerPrefs.GetInt("TitanKills", 0))
            PlayerPrefs.SetInt("TitanKills", titanKills);

        if (lintKills > PlayerPrefs.GetInt("LintKills", 0))
            PlayerPrefs.SetInt("LintKills", lintKills);

        if (flyingKills > PlayerPrefs.GetInt("FlyingKills", 0))
            PlayerPrefs.SetInt("FlyingKills", flyingKills);

        record.text = "Personal Bests:\n       Titan Kills: " + PlayerPrefs.GetInt("TitanKills", 0).ToString() + "\nRanged Enemy Kills: " + PlayerPrefs.GetInt("LintKills", 0).ToString() + "\nFlying Enemy Kills: " + PlayerPrefs.GetInt("FlyingKills", 0).ToString();
    }

    // Spawns a titan
    public void SpawnTitan()
    {
        if (player.activeInHierarchy)
        {
            int choice = Random.Range(0, 2);
            var tit = Instantiate(titanEnemy, Vector3.zero, Quaternion.identity, null);
            var titScript = tit.GetComponent<Basic_Titan>();
            titScript.survival = true;
            titScript.engageDistance = 10000;

            if (titanCount >= maxTitans - 4)
            {
                titScript.scalable = true;
                titScript.scalePreset = Random.Range(0.9f, 2f);
            }

            if (choice == 0)
            {
                tit.transform.position = spawnPointA.position + (Vector3.up * 20);
                tit.transform.eulerAngles = new Vector3(0, 180, 0);
                titScript.chaseDirection = 1;
            }
            else
            {
                tit.transform.position = spawnPointB.position + (Vector3.up * 20);
                tit.transform.eulerAngles = Vector3.zero;
                titScript.chaseDirection = -1;
            }
        }
    }

    // Spawns a lint
    public void SpawnLint()
    {
        if (player.activeInHierarchy)
        {
            int choice = Random.Range(0, 2);
            var lin = Instantiate(lintEnemy, Vector3.zero, Quaternion.identity, null);
            lin.transform.GetChild(0).GetComponent<Weakspot_Of_The_Forbidden_One>().survival = true;
            lin.GetComponent<SwarmScript>().engageDistance = 1500;

            if (choice == 0)
            {
                lin.transform.position = spawnPointA.position;
            }
            else
            {
                lin.transform.position = spawnPointB.position;
            }
        }
    }

    // Spawns a flying enemy
    public void SpawnFlying()
    {
        if (player.activeInHierarchy)
        {
            int choice = Random.Range(0, 2);
            var fly = Instantiate(flyingEnemy, Vector3.zero, Quaternion.identity, null);
            fly.transform.GetChild(0).GetComponent<Weakspot_Of_The_Forbidden_One>().survival = true;

            var flyScript = fly.GetComponent<TheFlyingOne>();
            flyScript.targetPos1 = new Vector3(900, 85, 0);
            flyScript.targetPos1 = new Vector3(-300, 85, 0);

            //if (choice == 0)
            //{
            //    fly.transform.position = spawnPointA.position + (Vector3.up * 100);
            //    flyScript.moveRight = true;
            //}
            //else
            {
                fly.transform.position = spawnPointB.position + (Vector3.up * 85);
                flyScript.moveRight = false;
            }
        }
    }

    // These coroutines increase the number of enemies that can spawn
    IEnumerator IncreaseTitans()
    {
        yield return new WaitForSeconds(titanIncreaseTime);

        if (titanCount < maxTitans)
        {
            titanCount++;
            SpawnTitan();
            StartCoroutine(IncreaseTitans());
        }
    }
    IEnumerator IncreaseLints()
    {
        yield return new WaitForSeconds(lintIncreaseTime);

        if (lintCount < maxLints)
        {
            lintCount++;
            SpawnLint();
            StartCoroutine(IncreaseLints());
        }
    }
    IEnumerator IncreaseFlying()
    {
        yield return new WaitForSeconds(flyingIncreaseTime);

        if (flyingCount < maxFlying)
        {
            flyingCount++;
            SpawnFlying();
            StartCoroutine(IncreaseFlying());
        }
    }
}
