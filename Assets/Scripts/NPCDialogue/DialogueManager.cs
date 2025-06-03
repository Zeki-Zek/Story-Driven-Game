using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    [SerializeField] GameObject canvas;
    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;

    public float typingSpeed = 0.2f;

    /*public Animator animator;*/

    private void Start()
    {
        QuestCanvas();
        /*canvas.SetActive(false);*/
        if (instance == null)
            instance = this;

        lines = new Queue<DialogueLine>();
    }

    IEnumerator QuestCanvas()
    {
        canvas.SetActive(true);
        yield return new WaitForSeconds(0f);
        /*canvas.SetActive(false);*/

    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;
        canvas.SetActive(true);

        /* animator.Play("show");*/

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue(); 
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));

    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        canvas.SetActive(false);
        /*animator.Play("hide");*/
    }
}
