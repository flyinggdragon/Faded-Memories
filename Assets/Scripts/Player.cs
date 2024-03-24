using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private float horizontal;
    public float speed = 6f;
    public Rigidbody2D rb;
    LevelManager levelManager;

    void Start()
    {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontal * speed, 0);

        if (transform.position.x > 9) {
            if (levelManager.currentLevel == "Street") {
                levelManager.switchScene("AlleyOutside");
                levelManager.currentLevel = "AlleyOutside";
            }

            if (levelManager.currentLevel == "AlleyOutside") {
                levelManager.switchScene("Park");
                levelManager.currentLevel = "Park";

            }
        }

        if (transform.position.x < -9) {
            if (levelManager.currentLevel == "AlleyOutside") {
                levelManager.switchScene("Street");
                levelManager.currentLevel = "Street";
            }

            else if (levelManager.currentLevel == "Park") {
                levelManager.switchScene("AlleyOutside");
                levelManager.currentLevel = "AlleyOutside";
            }
        }
    }  
}