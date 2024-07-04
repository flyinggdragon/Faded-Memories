using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour {
    [SerializeField] public List<DialogueString> dialogueStrings = new List<DialogueString>();

    public bool firstInteraction = true;

    public void DialogueStart() {
        NPC npcComponent = transform.parent?.GetComponent<NPC>();
        string npcName = npcComponent?.npcName;

        if (npcName == null) {
            npcName = "";
        }

        Player.Instance.dialogueManager.DialogueStart(dialogueStrings, npcName, firstInteraction);
        firstInteraction = false;
    }
}

[System.Serializable]
public class DialogueString {
    public string text;
    public bool isEnd;
    
    [Header("Branch")]
    public bool isQuestion;
    public string answerOption1; 
    public bool highlightOption1;
    public string answerOption2; 
    public bool highlightOption2;
    public string answerOption3; 
    public bool highlightOption3; 
    public string answerOption4; 
    public bool highlightOption4; 
    public string answerOption5; 
    public bool highlightOption5; 
    public int option1IndexJump;
    public int option2IndexJump;
    public int option3IndexJump;
    public int option4IndexJump;
    public int option5IndexJump;

    [Header("Triggered Events")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;

    [Header("Audio")]
    public AudioClip dialogueSoundEffect;
}