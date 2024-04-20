using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Player : MonoBehaviour {
    private static Player instance;

    private ElementContainer elementContainer;
    private Notebook notebook;
    private DialogueTrigger dialogueTrigger;
    private DialogueManager dialogueManager;
    private UIManager uiManager;
    private CluesManager cluesManager;
    private LevelManager levelManager;


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
   

    void Start() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        elementContainer = GameObject.Find("Element Container").GetComponent<ElementContainer>();

        notebook = elementContainer.notebook;
        cluesManager = elementContainer.cluesManager;
        dialogueTrigger = elementContainer.dialogueTrigger;
        dialogueManager = this.GetComponent<DialogueManager>();
        uiManager = elementContainer.uiManager;
        levelManager = elementContainer.levelManager;
    }

    void Update() {
        if (dialogueManager.IsDialoguing || notebook.IsOpen || uiManager.modalOpen) {
            haltMovement = true;
        } else { haltMovement = false; }

        if (!haltMovement) {
            horizontal = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontal * speed, 0);
        }

        float newX = Mathf.Clamp(transform.position.x, screenLimitLeft, screenLimitRight);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (!dialogueManager.IsDialoguing && !uiManager.modalOpen) {
                notebook.ToggleNotebook();
            }
        }

        if (Input.GetKeyDown(KeyCode.E) && inTrigger && !dialogueManager.IsDialoguing && !uiManager.modalOpen) {            
            if (triggerType == "NPC") {
                dialogueTrigger.StartDialogue();
            }

            else if (triggerType == "Item") {
                CluesManager.Item item = cluesManager.FindItem(otherName);
                string info = "\n" + item.itemName + "\n" + item.description + "\n" + item.keyword;

                uiManager.CreateSimpleModal("Você coletou " + item.itemName + info, "Item pego!");
                cluesManager.CollectItem(item);

                Destroy(GameObject.Find(otherName));
            }
        }

        // Verificação para troca de cenas
        if (transform.position.x >= screenLimitRight) {
            if (!string.IsNullOrEmpty(levelManager.currentLevel.rightName)) {
                levelManager.ExitRight();
            }
        }

        if (transform.position.x <= screenLimitLeft) {
            if (!string.IsNullOrEmpty(levelManager.currentLevel.leftName)) {
                levelManager.ExitLeft();
            }
        }
        
        // Verificar também se está no trigger para tal.
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            if (!string.IsNullOrEmpty(levelManager.currentLevel.upName)) {
                levelManager.ExitUp();
            }
        }

        // Verificar também se está no trigger para tal.
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            if (!string.IsNullOrEmpty(levelManager.currentLevel.downName)) {
                levelManager.ExitDown();
            }
        }

        if (inTrigger == true) {
            PopUp.SetActive(true);
        }

        else {
            PopUp.SetActive(false);
        }
    }
    
    public static Player Instance {
        get {
            return instance;
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