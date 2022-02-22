using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target_Counter : MonoBehaviour
{
    public int targetCount;
    private int currentCount = 0;

    public Text targetCounter;

    public SpriteRenderer barrier;
    public SpriteRenderer mask;
    public GameObject barrierObj;
    private float opacity = 1.0f;
    private bool completed;

    public Sequencer stageSequencer;

    private void Awake()
    {
        targetCounter.text = currentCount.ToString() + "/" + targetCount.ToString() + " targets destroyed";
    }

    private void Update()
    {
        if (opacity > 0 && completed)
        {
            opacity -= Time.deltaTime;
            barrier.color = new Color(1, 1, 1, opacity);
            mask.color = new Color(1, 1, 1, opacity);
        }
        else if (completed)
        {
            Destroy(barrierObj);
            Destroy(mask);
            completed = false;
        }
    }

    public void AddTargetDown()
    {
        currentCount++;

        targetCounter.text = currentCount.ToString() + "/" + targetCount.ToString() + " targets destroyed";

        if (currentCount == targetCount)
        {
            completed = true;
        }
    }
}
