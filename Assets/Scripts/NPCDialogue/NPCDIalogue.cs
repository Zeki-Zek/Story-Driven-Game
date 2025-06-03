using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCDialogue", menuName ="NPC Dialogue")]
public class NPCDIalogue : ScriptableObject
{
    public string npcName;
    public Sprite NPCSprite;
    public string[] dialogueLines;
    public bool[] autoProgressLine;
    public float autoProgressDelay;
    public float typeSpeed = 1f;
    public AudioClip voiceSound;
    public float voicePitch = 1f;
    
    
}
