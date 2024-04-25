using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDialogue : MonoBehaviour {

    public static CanvasDialogue Instance { get; private set; }
   
    void Awake() {
        if (Instance == null && Instance != this) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {
            Destroy(gameObject);
        }
    }
}