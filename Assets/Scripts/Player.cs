using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float horizontal;
    public float speed = 6f;
    public Rigidbody2D rb;
    private float screenLimitLeft = -9f;
    private float screenLimitRight = 9f;

    void Start() {
        
    }

    void Update() {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, 0);

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
    }
}