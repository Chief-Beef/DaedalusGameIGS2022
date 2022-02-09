using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attacks : MonoBehaviour
{
    public Animator anm;
    private bool canAttack = true;

    void Update()
    {
        if (Input.GetAxis("Fire1") == 1 && canAttack)
        {
            anm.Play("QuickSwing");
            canAttack = false;
        }
        else if (canAttack)
            anm.Play("Idle");
    }

    public void CheckAttack()
    {
        if (Input.GetAxis("Fire1") == 1)
            anm.Play("ContinueSwing");
        else
            canAttack = true;
    }

    public void CanAttack()
    {
        canAttack = true;
    }
}
