using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour {
    private DialogueTrigger dialogueTrigger;
    public DialogueManager dialogueManager;


    private float horizontal;
    public float speed = 6f;
    public Rigidbody2D rb;
    public float screenLimitLeft = -7.2f;
    public float screenLimitRight = 6.1f;
    private bool inTrigger = false;
    private Collider2D currentTrigger;
    public GameObject HoverPopUp;
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

        float newX = Mathf.Clamp(transform.position.x, screenLimitLeft, screenLimitRight);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (!dialogueManager.IsDialoguing && !UIManager.Instance.modalOpen) {
                Notebook.Instance.ToggleNotebook();
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && inTrigger && !dialogueManager.IsDialoguing && !UIManager.Instance.modalOpen) {            
            if (currentTrigger.CompareTag("NPC")) {
                dialogueTrigger = currentTrigger.GetComponent<DialogueTrigger>();
                dialogueTrigger?.StartDialogue();
            }

            else if (currentTrigger.CompareTag("Item")) {
                var item = CluesManager.Instance.FindItem(currentTrigger.name);

                CluesManager.Instance.CollectItem(item);
                Destroy(GameObject.Find(currentTrigger.name));
            }
        }

        if (transform.position.x >= screenLimitRight) {
            if (!string.IsNullOrEmpty(GameManager.currentLevel.rightName)) {
                LevelManager.Instance.ExitRight();
            }
        }

        if (transform.position.x <= screenLimitLeft) {
            if (!string.IsNullOrEmpty(GameManager.currentLevel.leftName)) {
                LevelManager.Instance.ExitLeft();
            }
        }
        
        // Verificar também se está no trigger para tal.
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && inTrigger) {
            if (currentTrigger.CompareTag("TriggerUp")) {
                LevelManager.Instance.ExitUp();
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

            else if (currentTrigger.CompareTag("TriggerUp")) {
                sprite = Resources.Load<Sprite>("Sprites/key_w");
            }
            
            HoverPopUp.SetActive(true);

            if (sprite) {
                HoverPopUp.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }

        else {
            HoverPopUp.SetActive(false);
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