using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target_Counter : MonoBehaviour
{   
    // Keeps count of how many targets are left
    public int targetCount;
    private int currentCount = 0;

    // Text box to display targets remaining
    public Text targetCounter;

    //
    public SpriteRenderer barrier;
    public SpriteRenderer mask;
    public GameObject barrierObj;
    private float opacity = 1.0f;
    private bool completed;

    private void Awake()
    {
        targetCounter.text = currentCount.ToString() + "/" + targetCount.ToString() + " targets destroyed";
    }

    private void Update()
    {
        // Fades text and barriers out
        if (opacity > 0 && completed)
        {
            opacity -= Time.deltaTime;
            if (barrier != null)
                barrier.color = new Color(barrier.color.r, barrier.color.g, barrier.color.b, opacity);

            if (mask != null)
                mask.color = new Color(mask.color.r, mask.color.g, mask.color.b, opacity);

            targetCounter.color = new Color(targetCounter.color.r, targetCounter.color.g, targetCounter.color.b, opacity);
        }
        // Destroys the barrier and mask objects once they faded out all the way
        else if (completed)
        {
            Destroy(barrierObj);
            Destroy(mask);
            completed = false;
        }
    }

    // Called when a target is destroyed
    public void AddTargetDown()
    {
        currentCount++;

        targetCounter.text = currentCount.ToString() + "/" + targetCount.ToString() + " targets destroyed";

        if (currentCount == targetCount)
        {
            completed = true;
            targetCounter.text = "";
        }
    }

    // Create new set of targets
    public void AddNewTargets(int newAmount)
    {
        currentCount = 0;
        targetCount = newAmount;
        targetCounter.text = currentCount.ToString() + "/" + targetCount.ToString() + " targets destroyed";
    }
}
