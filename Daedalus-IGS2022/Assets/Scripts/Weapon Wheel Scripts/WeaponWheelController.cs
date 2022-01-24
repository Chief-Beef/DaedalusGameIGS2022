using UnityEngine;
using UnityEngine.UI;

public class WeaponWheelController : MonoBehaviour
{
    public Animator anim;
    private bool weaponWheelSelected = false;
    public Image selectedItem;
    public Sprite noImage;
    public static int weaponID;
   
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            weaponWheelSelected = !weaponWheelSelected;
        }

        if (weaponWheelSelected)
        {
            anim.SetBool("OpenWeaponWheel", true);
        }
        else
        {
            anim.SetBool("OpenWeaponWheel", false);
        }

        switch (weaponID)
        {
            //no selection
            case 0:
                selectedItem.sprite = noImage;
                break;
            //basic weapon
            case 1:
                Debug.Log("GUN");
                break;
            //ice ice baby
            case 2:
                Debug.Log("ICE");
                break;
            //poison weapon
            case 3:
                Debug.Log("POISON");
                break;
            //fire weapon
            case 4:
                Debug.Log("FIRE");
                break;
        }
    }
}
