using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour {
    [SerializeField] private List<DialogueString> dialogueStrings = new List<DialogueString>();

    //private bool hasSpoken = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player") /*&& !hasSpoken*/) {
            other.gameObject.GetComponent<DialogueManager>().DialogueStart(dialogueStrings);

            //hasSpoken = true;
        }
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
}