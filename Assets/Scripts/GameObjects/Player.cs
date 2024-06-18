using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour {
    public DialogueManager dialogueManager;


    private float horizontal;
    private float speed = 8.0f;
    public Rigidbody2D rb;
    private bool inTrigger = false;
    private Collider2D currentTrigger;
    public GameObject hoverPopUp;
    private int orientation = 1;
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

        if (rb.velocity.x > 0) { orientation = 1; }
        else if (rb.velocity.x < 0) { orientation = -1; }

        transform.localScale = new Vector3(orientation, transform.localScale.y, transform.localScale.z);

        if (currentTrigger != null && currentTrigger.CompareTag("TriggerRight")) {
            if (Input.GetKeyDown(KeyCode.D)) {
                Exit exit = currentTrigger.gameObject.GetComponent<Exit>();
                if (!string.IsNullOrEmpty(exit.nextLevel)) {
                    LevelManager.Instance.LoadLevel(exit.nextLevel);

                    transform.position = new Vector3(
                        exit.nextPosX,
                        exit.nextPosY,
                        transform.position.z
                    );

                    transform.localScale = new Vector3(
                        exit.nextScaleX,
                        transform.localScale.y,
                        transform.localScale.z
                    );
                }
            }
        }

        if (currentTrigger != null && currentTrigger.CompareTag("TriggerLeft")) {
            if (Input.GetKeyDown(KeyCode.A)) {
                Exit exit = currentTrigger.gameObject.GetComponent<Exit>();
                if (!string.IsNullOrEmpty(exit.nextLevel)) {
                    LevelManager.Instance.LoadLevel(exit.nextLevel);

                    transform.position = new Vector3(
                        exit.nextPosX,
                        exit.nextPosY,
                        transform.position.z
                    );

                    transform.localScale = new Vector3(
                        exit.nextScaleX,
                        transform.localScale.y,
                        transform.localScale.z
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
                Exit exit = currentTrigger.gameObject.GetComponent<Exit>();
                if (exit == null) {
                    HomeEntrance he = currentTrigger.gameObject.GetComponent<HomeEntrance>();
                    he.Enter();
                } 
                else {
                    if (!string.IsNullOrEmpty(exit?.nextLevel)) {
                        LevelManager.Instance.LoadLevel(exit.nextLevel);

                        transform.position = new Vector3(
                            exit.nextPosX,
                            exit.nextPosY,
                            transform.position.z
                        );

                        transform.localScale = new Vector3(
                            exit.nextScaleX,
                            transform.localScale.y,
                            transform.localScale.z
                        );
                    }
                }
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

            Exit exit = currentTrigger.gameObject.GetComponent<Exit>();
            if (sprite && !string.IsNullOrEmpty(exit?.nextLevel)) {
                hoverPopUp.GetComponent<SpriteRenderer>().sprite = sprite;
            }

            Transform ht = hoverPopUp.transform;
            ht.localScale = new Vector3(orientation * 4.75f, ht.transform.localScale.y, ht.transform.localScale.z);
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