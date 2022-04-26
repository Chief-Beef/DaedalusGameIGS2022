using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICol : MonoBehaviour
{
    public CollectableScript CollectableScript;
    public bool collected = false;
    //public GameObject collectable;
    public SpriteRenderer collectable;
    public Image image;
    public Color32 color;

    // Start is called before the first frame update
    void Start()
    {
        //collectable is the gameobject that corresponds to the collectable
        //collectable = GameObject.Find("/C1/C2/C3/C4/C5/C6/C7");
        //CollectableScript = 
        collectable = collectable.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (collected)
        {
            //turn image red
            image.GetComponent<Image>().color = Color.white;
            Destroy(collectable);
        }
    }
}
