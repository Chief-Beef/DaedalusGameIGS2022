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
    // Things that don't move because they're so far away, like the sun for example
    public GameObject staticBG;
    private Vector3 staticStartPos;
    //
    public GameObject player;

    private void Start()
    {
        frontStartPos = frontBG.transform.position;
        rearStartPos = rearBG.transform.position;
        staticStartPos = staticBG.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
