using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    public void UpdateLevel(int level)
    {
        int currentLevel = PlayerPrefs.GetInt("Level", 0);
        if (level > currentLevel)
        { PlayerPrefs.SetInt("Level", level); }
    }
}
