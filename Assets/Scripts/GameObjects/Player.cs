using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour {
    private ElementContainer elementContainer;
    private DialogueTrigger dialogueTrigger;
    private DialogueManager dialogueManager;


    private float horizontal;
    public float speed = 6f;
    public Rigidbody2D rb;
    public float screenLimitLeft = -7.2f;
    public float screenLimitRight = 6.1f;
    private bool inTrigger = false;
    private string otherName = "";
    private string triggerType;
    private bool haltMovement = false;
    public GameObject PopUp;
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
        elementContainer = GameObject.Find("Element Container").GetComponent<ElementContainer>();

        dialogueTrigger = elementContainer.dialogueTrigger;
        dialogueManager = this.GetComponent<DialogueManager>();
        
    }

    void Update() {
        if (dialogueManager.IsDialoguing || Notebook.Instance.isOpen || UIManager.Instance.modalOpen) {
            haltMovement = true;
        } else { haltMovement = false; }

        if (!haltMovement) {
            horizontal = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontal * speed, 0);
        }

        float newX = Mathf.Clamp(transform.position.x, screenLimitLeft, screenLimitRight);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (!dialogueManager.IsDialoguing && !UIManager.Instance.modalOpen) {
                Notebook.Instance.ToggleNotebook();
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && inTrigger && !dialogueManager.IsDialoguing && !UIManager.Instance.modalOpen) {            
            if (triggerType == "NPC") {
                dialogueTrigger.StartDialogue();
            }

            else if (triggerType == "Item") {
                var item = CluesManager.Instance.FindItem(otherName);
                string info = "\n" + item.itemName + "\n" + item.description + "\n" + item.keyword;

                UIManager.Instance.CreateSimpleModal("Você coletou " + item.itemName + info, "Item pego!");
                CluesManager.Instance.CollectItem(item);

                Destroy(GameObject.Find(otherName));
            }
        }

        // Verificação para troca de cenas
        if (transform.position.x >= screenLimitRight) {
            if (!string.IsNullOrEmpty(LevelManager.Instance.currentLevel.rightName)) {
                LevelManager.Instance.ExitRight();
            }
        }

        if (transform.position.x <= screenLimitLeft) {
            if (!string.IsNullOrEmpty(LevelManager.Instance.currentLevel.leftName)) {
                LevelManager.Instance.ExitLeft();
            }
        }
        
        // Verificar também se está no trigger para tal.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            if (!string.IsNullOrEmpty(LevelManager.Instance.currentLevel.upName)) {
                LevelManager.Instance.ExitUp();
            }
        }

        // Verificar também se está no trigger para tal.
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            if (!string.IsNullOrEmpty(LevelManager.Instance.currentLevel.downName)) {
                LevelManager.Instance.ExitDown();
            }
        }

        if (inTrigger == true) {
            PopUp.SetActive(true);
        }

        else {
            PopUp.SetActive(false);
        }
    }

   private void OnTriggerEnter2D(Collider2D other) {
        triggerType = other.tag;
        inTrigger = true;
        otherName = other.gameObject.name;
    }

    private void OnTriggerExit2D(Collider2D other) {
        triggerType = null;
        inTrigger = false;
        otherName = "";
    }
}