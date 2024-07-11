using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour {
    public DialogueManager dialogueManager;
    public Notebook notebook;
    public BlackScreenText bst;
    public Pause pause;
    public Animator animator;
    public static Player Instance { get; private set; }
    public Rigidbody2D rb;
    private Collider2D currentTrigger;
    public GameObject hoverPopUp;
    public bool shouldMove = true;
    public bool shouldOpenNotebook = true;
    public bool shouldDialogue = true;
    public bool shouldPause = true;
    private float horizontal;
    private float speed = 10.0f;
    private int orientation = 1;
    private bool inTrigger = false;
   
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
        animator = GetComponent<Animator>();
    }

    void Update() {
        UIManager.Instance.UnlockCursor();

        animator.SetFloat("xVelocity", Math.Abs(rb.velocity.x));

        if (shouldMove) {
            horizontal = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontal * speed, 0);
        } else { rb.velocity = Vector2.zero; }

        if (rb.velocity.x > 0) { orientation = 1; }
        else if (rb.velocity.x < 0) { orientation = -1; }

        transform.localScale = new Vector3(orientation, transform.localScale.y, transform.localScale.z);

        if (currentTrigger != null && currentTrigger.CompareTag("TriggerRight")) {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
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
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
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
            if (shouldOpenNotebook) {
                notebook.ToggleNotebook();
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && inTrigger && shouldDialogue) {            
            if (currentTrigger.CompareTag("NPC")) {
                currentTrigger.GetComponent<DialogueTrigger>().DialogueStart();
            }

            else if (currentTrigger.CompareTag("Item")) {
                if (CluesManager.Instance.cells.Count > 0) { 
                    var item = CluesManager.Instance.FindItem(currentTrigger.name);
                    
                    CluesManager.Instance.CollectItem(item);
                    Destroy(GameObject.Find(currentTrigger.name));

                    if (item.itemName == "200 Rukh Bill") {
                        GameManager.money += 200;
                    }

                    if (item.itemName == "Note") {
                        AlleyStart.noteCollected = true;
                    }
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

        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (shouldPause) {
                Pause.Instance.TogglePause();
            }
        }

        if (inTrigger == true) {
            Sprite sprite = null;
            Exit exit = currentTrigger.gameObject.GetComponent<Exit>();

            if (currentTrigger.CompareTag("Item") || currentTrigger.CompareTag("NPC") || currentTrigger.CompareTag("Puzzle")) {
                sprite = Resources.Load<Sprite>("Sprites/key_e");
            }

            else if (currentTrigger.CompareTag("TriggerRight")) {
                if (!string.IsNullOrEmpty(exit.nextLevel)) {
                    sprite = Resources.Load<Sprite>("Sprites/key_d");
                }
            }

            else if (currentTrigger.CompareTag("TriggerLeft")) {
                if (!string.IsNullOrEmpty(exit.nextLevel)) {
                    sprite = Resources.Load<Sprite>("Sprites/key_a");
                }
            }

            else if (currentTrigger.CompareTag("TriggerUp")) {
                sprite = Resources.Load<Sprite>("Sprites/key_w");
            }

            if (sprite) {
                hoverPopUp.GetComponent<SpriteRenderer>().sprite = sprite;
            }

            Transform ht = hoverPopUp.transform;
            ht.localScale = new Vector3(orientation * 4.75f, ht.transform.localScale.y, ht.transform.localScale.z);
            
            if (!shouldDialogue) {
                hoverPopUp.GetComponent<SpriteRenderer>().sprite = null;
            }

            hoverPopUp.SetActive(true);
        }

        else {
            hoverPopUp.SetActive(false);
            hoverPopUp.GetComponent<SpriteRenderer>().sprite = null;
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

    public void ToggleMovement(bool should) {
        shouldOpenNotebook = should;
        shouldPause = should;
        shouldDialogue = should;
        shouldMove = should;
    }
}