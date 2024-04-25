using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour {
    [SerializeField] private List<DialogueString> dialogueStrings = new List<DialogueString>();
    private DialogueManager dialogueManager;

    void Start() {
        dialogueManager = Player.Instance.dialogueManager;
    }
    
    public void StartDialogue() {
        // Talvez possa dar problema dependendo, se tiver mais de um DlgMngr? Por ora OK.
        dialogueManager.DialogueStart(dialogueStrings);
    }
}

[System.Serializable]
public class DialogueString {
    public string text;
    public bool isEnd;
    
    [Header("Branch")]
    public bool isQuestion;
    public string answerOption1;
    public string answerOption2; 
    public int option1IndexJump;
    public int option2IndexJump;

    [Header("Triggered Events")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;

    [Header("Audio")]
    public AudioClip dialogueSoundEffect;
}