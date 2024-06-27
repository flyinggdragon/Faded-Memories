using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeEntrance : MonoBehaviour {
    private bool canEnter = GameManager.items[2].collected;
    private string nextLevel;

    void Start() {
        nextLevel = "Home_Inside";
    }

    void Update() {
        if (GameManager.escaping) {
            nextLevel = "Home_Inside_4";
        } 
        
        else if (GameManager.secondQuarterCompleted) {
            nextLevel = "Home_Inside_3";
        }
    }

    public void Enter() {
        if (!canEnter) {
            AudioManager.Instance.PlaySound(AudioManager.Instance.doorLock);
            UIManager.Instance.CreateToastModal("You may not enter without the key.", "Access denied!");
        }

        else {
            LevelManager.Instance.LoadLevel(nextLevel);

            transform.position = new Vector3(
                        -11.0f,
                        -2.19f,
                        transform.position.z
                    );

                    transform.localScale = new Vector3(
                        1.0f,
                        transform.localScale.y,
                        transform.localScale.z
                    );
        }
    }
}
