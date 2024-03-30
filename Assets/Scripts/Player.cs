using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {
    private Notebook notebookScript;
    private float horizontal;
    public float speed = 6f;
    public Rigidbody2D rb;
    private float screenLimitLeft = -9f;
    private float screenLimitRight = 9f;

    void Start() {
        notebookScript = GameObject.Find("Notebook Holder").GetComponent<Notebook>();
        GameObject.Find("Notebook Holder").SetActive(false);
    }

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, 0);

        if (Input.GetKeyDown(KeyCode.Q)) {
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

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.tag == "Item") {
            Destroy(collider.gameObject);
        }
    }
}