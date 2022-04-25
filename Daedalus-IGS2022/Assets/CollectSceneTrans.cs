using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSceneTrans : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void fadeOut()
    {
        GameObject.FindGameObjectWithTag("Fade").GetComponent<FadeIn>().FadeOut(2);
      
    }
}
