using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlleyStart : MonoBehaviour {
    Exit rightTrigger;
    CluesManager.Item note;
    public static bool firstTimeIn = true;
    public static bool noteCollected = false;

    void Start() {
        rightTrigger = GameObject.Find("ExitRight").GetComponent<Exit>();
        
        if (firstTimeIn) {
            Notebook.Instance.ToggleNotebook();
        }
    }

    void Update() {
        if (!noteCollected) {
            rightTrigger.nextLevel = "";
        } else {
            rightTrigger.nextLevel = "Street";
        }
    }
}