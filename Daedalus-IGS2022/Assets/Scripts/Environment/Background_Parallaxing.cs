using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_Parallaxing : MonoBehaviour
{
    // Stuff closer to the player that may move a little
    public GameObject[] backgrounds;
    public Vector3[] startPositions;
    public float[] movementMultpliersX;
    public float[] movementMultipliersY;

    // Player for reference
    public Transform player;
    public Transform ragdoll;
    private Vector3 playerStartPos;

    private void Start()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            startPositions[i] = backgrounds[i].transform.position;
        }

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        playerStartPos = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Initializes difference variable
        var diff = Vector2.zero;

        // Sets difference to be based on player or ragdoll position
        // based on which one is currently in use
        if (player.gameObject.activeInHierarchy)
            diff = player.position - playerStartPos;
        else
            diff = ragdoll.position - playerStartPos;

        // Moves backgrounds at specified multiplier factors
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].transform.position = new Vector2(startPositions[i].x + diff.x * movementMultpliersX[i],
                                                            startPositions[i].y + diff.y * movementMultipliersY[i]);
        }
    }
}
