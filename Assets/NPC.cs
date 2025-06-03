using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class NPC : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public NPCDIalogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;



    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null || (PauseController.isGamePaused && !isDialogueActive))
            return;

        if (isDialogueActive)
        {
            //next line
            NextLine();
        }
        else
        {
            //dialogue start
            StartDialogue();
        }

    }
    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typeSpeed);
        }
        isTyping = false;

        if (dialogueData.autoProgressLine.Length > dialogueIndex && dialogueData.autoProgressLine[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        PauseController.SetPause(false);

    }
    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.name);
        portraitImage.sprite = dialogueData.NPCSprite;

        dialoguePanel.SetActive(true);
        PauseController.SetPause(true);

        StartCoroutine(TypeLine());

    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);

            isTyping = false;
        }
        else if (++dialogueIndex + 1 < dialogueData.dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            //END
            EndDialogue();
        }


    }
}
