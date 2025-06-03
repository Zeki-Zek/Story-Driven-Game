using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;

}

[System.Serializable]
public class DialogueLine
{ 
    public DialogueCharacter character;
    [TextArea(3,10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    
    public void TriggerDialogue()
    {
        if (DialogueManager.instance == null)
            Debug.LogError("DialogueManager.instance is null");

        if (dialogue == null)
            Debug.LogError("Dialogue is not assigned on " + gameObject.name);
        DialogueManager.instance.StartDialogue(dialogue);
    }

     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone of: " + gameObject.name);

            TriggerDialogue();
        }
    }
}
