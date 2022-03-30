using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Parallaxing : MonoBehaviour
{
    // Stuff closer to the player that may move a little
    public GameObject frontBG;
    private Vector3 frontStartPos;
    // Stuff farther back that doesn't move as much
    public GameObject rearBG;
    private Vector3 rearStartPos;
    public float rearYOffset;
    // Things that don't move because they're so far away, like the sun for example
    public GameObject staticBG;
    private Vector3 staticStartPos;
    // Player for reference
    public Transform player;
    public Transform ragdoll;
    private Vector3 playerStartPos;

    private void Start()
    {
        frontStartPos = frontBG.transform.position;
        rearStartPos = rearBG.transform.position;
        staticStartPos = staticBG.transform.position;

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
        if (ragdoll == null)
            ragdoll = GameObject.FindGameObjectWithTag("Ragdoll").transform;

        playerStartPos = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.activeInHierarchy)
        {
            Vector3 diff = player.position - playerStartPos;

            if (frontBG != null)
                frontBG.transform.position = new Vector2(frontStartPos.x + diff.x / 8, frontStartPos.y + diff.y / 24);
            if (rearBG != null)
                rearBG.transform.position = new Vector2(rearStartPos.x + (diff.x / 1.5f), rearStartPos.y + rearYOffset + (diff.y / 2.5f));
            if (staticBG != null)
                staticBG.transform.position = new Vector2(staticStartPos.x + diff.x / 1.05f, staticStartPos.y + diff.y / 1.05f);
        }
        else
        {
            Vector3 diff = ragdoll.position - playerStartPos;

            frontBG.transform.position = ragdoll.position - diff;
            rearBG.transform.position = ragdoll.position - diff;
            staticBG.transform.position = ragdoll.position - diff;
        }
    }
}
