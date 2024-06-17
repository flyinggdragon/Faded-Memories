using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour {
    public DialogueManager dialogueManager;


    private float horizontal;
    private float speed = 25.0f;
    public Rigidbody2D rb;
    private bool inTrigger = false;
    private Collider2D currentTrigger;
    public GameObject hoverPopUp;
    public static Player Instance { get; private set; }
   
    void Awake() {
        if (Instance == null && Instance != this) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        dialogueManager = this.GetComponent<DialogueManager>();
    }

    void Update() {
        if (!(dialogueManager.IsDialoguing || Notebook.Instance.isOpen || UIManager.Instance.modalOpen)) {
            horizontal = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontal * speed, 0);
        } else { rb.velocity = Vector2.zero; }

        if (currentTrigger != null && currentTrigger.CompareTag("TriggerRight")) {
            if (!string.IsNullOrEmpty(GameManager.currentLevel.rightName)) { 
                if (Input.GetKeyDown(KeyCode.D)) {
                    LevelManager.Instance.ExitRight();

                    transform.position = new Vector3(
                        0.0f,
                        transform.position.y,
                        transform.position.z
                    );
                }
            }
        }

        if (currentTrigger != null && currentTrigger.CompareTag("TriggerLeft")) {
            if (!string.IsNullOrEmpty(GameManager.currentLevel.leftName)) {
                if (Input.GetKeyDown(KeyCode.A)) {
                    LevelManager.Instance.ExitLeft();

                    transform.position = new Vector3(
                        0.0f,
                        transform.position.y,
                        transform.position.z
                    );
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (!dialogueManager.IsDialoguing && !UIManager.Instance.modalOpen) {
                Notebook.Instance.ToggleNotebook();
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && inTrigger && !dialogueManager.IsDialoguing && !UIManager.Instance.modalOpen) {            
            if (currentTrigger.CompareTag("NPC")) {
                currentTrigger.GetComponent<DialogueTrigger>().DialogueStart();
            }

            else if (currentTrigger.CompareTag("Item")) {
                if (CluesManager.Instance.cells.Count > 0) { 
                    var item = CluesManager.Instance.FindItem(currentTrigger.name);
                    
                    CluesManager.Instance.CollectItem(item);
                    Destroy(GameObject.Find(currentTrigger.name));
                }
            }
        }
        
        // Verificar também se está no trigger para tal.
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && inTrigger) {
            if (currentTrigger.CompareTag("TriggerUp")) {
                LevelManager.Instance.ExitUp();

                transform.position = new Vector3(
                        0.0f,
                        transform.position.y,
                        transform.position.z
                    );
            }
        }

        // Verificar também se está no trigger para tal.
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            if (!string.IsNullOrEmpty(GameManager.currentLevel.downName)) {
                LevelManager.Instance.ExitDown();
            }
        }

        if (inTrigger == true) {
            Sprite sprite = null;

            if (currentTrigger.CompareTag("Item") || currentTrigger.CompareTag("NPC") || currentTrigger.CompareTag("Puzzle")) {
                sprite = Resources.Load<Sprite>("Sprites/key_e");
            }

            else if (currentTrigger.CompareTag("TriggerRight")) {
                sprite = Resources.Load<Sprite>("Sprites/key_d");
            }

            else if (currentTrigger.CompareTag("TriggerLeft")) {
                sprite = Resources.Load<Sprite>("Sprites/key_a");
            }

            else if (currentTrigger.CompareTag("TriggerDown")) {
                sprite = Resources.Load<Sprite>("Sprites/key_s");
            }

            else if (currentTrigger.CompareTag("TriggerUp")) {
                sprite = Resources.Load<Sprite>("Sprites/key_w");
            }
            
            hoverPopUp.SetActive(true);

            if (sprite) {
                hoverPopUp.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }

        else {
            hoverPopUp.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        currentTrigger = other;
        inTrigger = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        currentTrigger = null;
        inTrigger = false;
    }
}