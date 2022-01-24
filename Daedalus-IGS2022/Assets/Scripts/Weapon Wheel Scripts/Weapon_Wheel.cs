using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Wheel : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;

    public GameObject[] buttons;

    private bool isHeld = false;
    private bool isOpen = false;


    private void Start()
    {
        // Initializing weapon wheel (disabling buttons)
        for (int i = 0; i < buttons.Length; i++)
            buttons[i].gameObject.SetActive(false);
    }

    void Update()
    {
        // Open the weapon wheel
        if (Input.GetAxis("WeaponWheel") > 0 && !isHeld && !isOpen)
        {
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].gameObject.SetActive(true);
            isHeld = true;
            isOpen = true;
        }
        // Close the weapon wheel
        else if (Input.GetAxis("WeaponWheel") > 0 && !isHeld && isOpen)
        {
            for (int i = 0; i < buttons.Length; i++)
                buttons[i].gameObject.SetActive(false);
            isHeld = true;
            isOpen = false;
        }
        else if (Input.GetAxis("WeaponWheel") < 1)
            isHeld = false;
    }

    public void SwitchWeaponType(int newWeaponType)
    {
        player = GameObject.FindGameObjectWithTag("Player");

        var script = player.GetComponent<Player_Script>();
        script.weaponType = newWeaponType;
        Debug.Log(newWeaponType);
    }
}
