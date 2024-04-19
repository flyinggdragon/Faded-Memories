using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementContainer : MonoBehaviour {
    private static ElementContainer instance;

    public Notebook notebook;
    public Player player;
    public AudioManager audioManager;
    public CluesManager cluesManager;
    public UIManager uiManager;
    public DialogueTrigger dialogueTrigger;
    public DialogueManager dialogueManager;
    public LevelManager levelManager;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject); // Se já existir uma instância, destrói o objeto atual
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // Marca o ElementContainer como DontDestroyOnLoad

        GameObject notebookHolder = GameObject.Find("Notebook Holder");
        GameObject cluesContent = GameObject.Find("Clues Content");

        notebook = notebookHolder.GetComponent<Notebook>();
        cluesManager = cluesContent.GetComponent<CluesManager>();

        player = GameObject.Find("Player").GetComponent<Player>();
        audioManager = GameObject.Find("Audio Manager").GetComponent<AudioManager>();
        uiManager = GameObject.Find("UI Manager").GetComponent<UIManager>();
        dialogueTrigger = GameObject.Find("Dialogue Trigger").GetComponent<DialogueTrigger>();
        dialogueManager = player.GetComponent<DialogueManager>();
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
    }

    public static ElementContainer Instance {
        get {
            return instance;
        }
    }

    void Start() {
        cluesManager.StartRun();
    }
}