using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            SceneManager.LoadScene(0);
        if (Input.GetKeyDown(KeyCode.O))
            SceneManager.LoadScene(1);
        if (Input.GetKeyDown(KeyCode.P))
            SceneManager.LoadScene(2);
    }
}
