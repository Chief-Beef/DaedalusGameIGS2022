using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delete_Progress : MonoBehaviour
{
    public GameObject prompt;

    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
        prompt.SetActive(false);
    }

    public void ShowPrompt()
    {
        prompt.SetActive(true);
    }

    public void Remove_Prompt()
    {
        prompt.SetActive(false);
    }
}
