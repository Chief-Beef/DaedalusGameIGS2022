using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public GameObject titanEnemy;
    public GameObject lintEnemy;
    public GameObject flyingEnemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IncreaseTitans());
        StartCoroutine(IncreaseLints());
        StartCoroutine(IncreaseFlying());
    }

    // Spawns a titan
    public void SpawnTitan()
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
        }
        else
        {
            tit.transform.position = spawnPointB.position + (Vector3.up * 20);
        }
    }

    // Spawns a lint
    public void SpawnLint()
    {
        int choice = Random.Range(0, 2);
        var lin = Instantiate(lintEnemy, Vector3.zero, Quaternion.identity, null);
        lin.transform.GetChild(0).GetComponent<Weakspot_Of_The_Forbidden_One>().survival = true;
        lin.GetComponent<SwarmScript>().engageDistance = 1000;

        if (choice == 0)
        {
            lin.transform.position = spawnPointA.position;
        }
        else
        {
            lin.transform.position = spawnPointB.position;
        }
    }

    // Spawns a flying enemy
    public void SpawnFlying()
    {
        int choice = Random.Range(0, 2);
        var fly = Instantiate(flyingEnemy, Vector3.zero, Quaternion.identity, null);
        fly.transform.GetChild(0).GetComponent<Weakspot_Of_The_Forbidden_One>().survival = true;

        var flyScript = fly.GetComponent<TheFlyingOne>();
        flyScript.targetPos1 = new Vector3(900, 100, 0);
        flyScript.targetPos1 = new Vector3(-300, 100, 0);

        if (choice == 0)
        {
            fly.transform.position = spawnPointA.position + (Vector3.up * 100);
            flyScript.moveRight = true;
        }
        else
        {
            fly.transform.position = spawnPointB.position + (Vector3.up * 100);
            flyScript.moveRight = false;
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
