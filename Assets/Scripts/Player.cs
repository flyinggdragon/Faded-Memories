using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private Notebook notebookScript;
    private float horizontal;
    public float speed = 6f;
    public Rigidbody2D rb;
    private DialogueTrigger dialogueTrigger;
    private float screenLimitLeft = -9f;
    private float screenLimitRight = 9f;

    void Start() {
        notebookScript = GameObject.Find("Notebook Holder").GetComponent<Notebook>();
        GameObject.Find("Notebook Holder").SetActive(false);

        dialogueTrigger = GameObject.Find("Dialogue Trigger").GetComponent<DialogueTrigger>();
        if (dialogueTrigger == null) {
            Debug.LogError("dialogueTrigger não pôde ser encontrado na cena.");
        }
    }

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, 0);

        if (Input.GetKeyDown(KeyCode.E)) {
            if (notebookScript != null) {
                notebookScript.ToggleNotebook();
            }
        }

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

    private void OnTriggerStay2D(Collider2D collider) {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (collider.gameObject.tag == "Item") {
                Destroy(collider.gameObject);
            }

            if (collider.gameObject.tag == "NPC") {
                dialogueTrigger.StartDialogue(this.gameObject);
            }
        }
    }
}