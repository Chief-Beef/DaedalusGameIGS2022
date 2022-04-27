using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Sequencer : MonoBehaviour
{
    // This script can be used for a sequence of text boxes that appear on screen.
    // Simply attach a Text component to the script and add as many messages as you need to.
    // It will be triggered when a player enters the trigger.

    // Text box that text will apear in
    public Text messageText;
    // Array of messages to display
    public string[] messages;
    // Time it takes for a new character to appear
    public float appearTime;
    // Time it takes for text to disappear after finishing
    public float clearTime;
    // Time it takes for next mesage to appear after clearing
    public float continueTime;

    // These variables manage the text appearing and triggering
    private int index = 0;
    private int textIndex = 0;
    private bool triggered = false;

    // Set true if for a tutorial
    public bool tutorialMessage;
    private bool messageCompleted = false;

    // Secondary text box to tell player which input to continue
    public Text continueBox;
    private bool glowUp = true;
    private float opacity;

    public bool noMoreMessages;

    // Plays sound when text appears
    public AudioSource textSound;

    private void Awake()
    {
        textSound = GameObject.FindGameObjectWithTag("TextScrollSound").GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (tutorialMessage)
        {
            if (messageCompleted)
            {
                // This code handles the flashing 'continue' text
                if (glowUp) // Increase opacity
                {
                    continueBox.color = new Color(continueBox.color.r, continueBox.color.g, continueBox.color.b, opacity);
                    if (opacity < 1)
                    {
                        opacity += Time.deltaTime;
                    }
                    else
                        glowUp = false;
                }
                else // Decrease opacity
                {
                    continueBox.color = new Color(continueBox.color.r, continueBox.color.g, continueBox.color.b, opacity);
                    if (opacity > 0)
                    {
                        opacity -= Time.deltaTime;
                    }
                    else
                        glowUp = true;
                }
            }
            else
                continueBox.color = new Color(continueBox.color.r, continueBox.color.g, continueBox.color.b, 0);

            if (messageCompleted && Input.GetKeyDown(KeyCode.R))
            {
                messageCompleted = false;
                StartCoroutine(ClearText());
            }
        }
    }

    // Triggers text when player enters trigger
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.tag == "Player" && !triggered)
        {
            triggered = true;
            StartCoroutine(TextAppear());
        }
    }


    // This coroutine will cause text to appear until no more
    // text is left to display in the current message
    IEnumerator TextAppear()
    {
        yield return new WaitForSeconds(appearTime);

        // Adds the next character to the text box and plays a sound
        messageText.text = messageText.text + messages[index][textIndex];
        textSound.Play();

        // Will call coroutine again if there are still characters to display
        if (textIndex < messages[index].Length - 1)
        {
            textIndex++;
            StartCoroutine(TextAppear());
        }
        // Will begin clearing text once message has been fully displayed
        else
        {
            if (!tutorialMessage)
                StartCoroutine(ClearText());
            else
            {
                messageCompleted = true;
                opacity = 0;
            }
            textIndex = 0;
        }
    }

    // Clears text after completing a message
    IEnumerator ClearText()
    {
        yield return new WaitForSeconds(clearTime);

        // Empties text from text box
        messageText.text = "";

        // Will initiate a new message if there are more messages left to display
        if (index < messages.Length - 1)
        {
            index++;
            StartCoroutine(BeginNextText());
        }
        else
            noMoreMessages = true;
    }

    // Will start the next message
    IEnumerator BeginNextText()
    {
        yield return new WaitForSeconds(continueTime);
        StartCoroutine(TextAppear());
    }
}
