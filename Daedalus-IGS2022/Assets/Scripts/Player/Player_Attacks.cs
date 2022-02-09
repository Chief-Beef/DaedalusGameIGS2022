using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attacks : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator anm;
    public Player_Script playerScript;

    private bool canAttack = true;
    private bool isHolding = false;
    private bool isContinuing = false;
    private bool canDash = true;

    public float lungeForce;

    public GameObject cooldownBar;
    private float cooldownTime = 2.5f;
    private float currentTime = 2.5f;


    void Update()
    {
        if (Input.GetAxis("Fire1") == 1 && canAttack && !isHolding)
        {
            anm.Play("QuickSwing");
            canAttack = false;
            isHolding = true;
        }
        else if (Input.GetAxis("Fire3") == 1 && canDash)
        {
            if (!playerScript.grounded)
            {
                if (playerScript.isGrappling)
                    playerScript.ResetGrapple();

                playerScript.rb.velocity = Vector2.zero;
                canDash = false;
                currentTime = 0f;

                rb.AddForce(Vector3.Normalize(playerScript.mousePos - this.transform.position) * lungeForce, ForceMode2D.Impulse);
                anm.Play("QuickSwing");
            }
        }
        else if (canAttack)
        {
            anm.Play("Idle");

            if (Input.GetAxis("Fire1") == 0)
                isHolding = false;
        }

        if (currentTime < cooldownTime)
        {
            // Make cooldown bar appear
            cooldownBar.SetActive(true);
            // Recharge bar
            Mathf.Clamp(currentTime += Time.deltaTime, 0, cooldownTime);
            // Set bar scale
            cooldownBar.transform.localScale = new Vector2(currentTime / cooldownTime, 1);

            // Cooldown period has ended
            if (currentTime >= cooldownTime)
            {
                cooldownBar.SetActive(false);
                canDash = true;
            }
        }
    }



    public void CheckAttack()
    {
        if (Input.GetAxis("Fire1") == 1 && isHolding)
            anm.Play("ContinueSwing");
        else
            canAttack = true;
    }

    public void CanAttack()
    {
        canAttack = true;
    }
}
