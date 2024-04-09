using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementContainer : MonoBehaviour {
 
    public Notebook notebook;
    public Player player;
    public AudioManager audioManager;
    public CluesManager cluesManager;
    public UIManager uiManager;
    public DialogueTrigger dialogueTrigger;
    public DialogueManager dialogueManager;

    void Start() {
        GameObject notebookHolder = GameObject.Find("Notebook Holder");
        GameObject cluesContent = GameObject.Find("Clues Content");

        notebook = notebookHolder.GetComponent<Notebook>();
        cluesManager = cluesContent.GetComponent<CluesManager>();

        player = GameObject.Find("Player").GetComponent<Player>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        dialogueTrigger = GameObject.Find("Dialogue Trigger").GetComponent<DialogueTrigger>();
        dialogueManager = this.player.GetComponent<DialogueManager>();
    }
}
