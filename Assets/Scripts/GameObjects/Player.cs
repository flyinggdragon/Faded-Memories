using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private ElementContainer elementContainer;
    private Notebook notebook;
    private DialogueTrigger dialogueTrigger;
    private DialogueManager dialogueManager;
    private UIManager uiManager;
    private CluesManager cluesManager;


    private float horizontal;
    public float speed = 6f;
    public Rigidbody2D rb;
    private float screenLimitLeft = -9f;
    private float screenLimitRight = 9f;
    private bool inTrigger = false;
    private string otherName = "";
    private string triggerType;
    private bool haltMovement = false;

    void Start() {
        elementContainer = GameObject.Find("Element Container").GetComponent<ElementContainer>();

        notebook = elementContainer.notebook;
        cluesManager = elementContainer.cluesManager;
        dialogueTrigger = elementContainer.dialogueTrigger;
        dialogueManager = this.GetComponent<DialogueManager>();
        uiManager = elementContainer.uiManager;
    }


    void Update() {
        if (dialogueManager.IsDialoguing || notebook.IsOpen /*|| *modal.IsOpen*/) {
            haltMovement = true;
        } else { haltMovement = false; }

        // Define a velocidade de acordo. speed = 0f -> travar movimento
        speed = haltMovement ? 0f : 6f;
        
        // Movimentação padrão
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, 0);

        if (Input.GetKeyDown(KeyCode.Q)) {
            if (dialogueManager.IsDialoguing) { return; }

            notebook.ToggleNotebook();
        }

        if (Input.GetKeyDown(KeyCode.E) && inTrigger && !dialogueManager.IsDialoguing) {            
            if (triggerType == "NPC") {
                dialogueTrigger.StartDialogue();
            }

            else if (triggerType == "Item") {
                CluesManager.Item item = cluesManager.FindItem(otherName);
                string info = "\n" + item.itemName + "\n" + item.description;

                uiManager.CreateSimpleModal("Você coletou " + otherName + info, "Item pego!");
            }
        }

        // Verificação para troca de cenas
        if (transform.position.x >= screenLimitRight) {
            if (LevelManager.currentLevel == "Street") {
                LevelManager.switchScene("AlleyOutside");
            }

            if (LevelManager.currentLevel == "Park") {
                LevelManager.switchScene("Street");
            }
        }

        if (transform.position.x <= screenLimitLeft) {
            if (LevelManager.currentLevel == "AlleyOutside") {
                LevelManager.switchScene("Street");
            }

            if (LevelManager.currentLevel == "Street") {
                LevelManager.switchScene("Park");
            }
        }


        if (LevelManager.currentLevel == "AlleyInside") {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                LevelManager.switchScene("AlleyOutside");
            }
        }

        if (LevelManager.currentLevel == "AlleyOutside") {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                LevelManager.switchScene("AlleyInside");
            }
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