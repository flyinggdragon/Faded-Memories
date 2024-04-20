using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasDialogue : MonoBehaviour
{
    private static CanvasDialogue instance;

    public static CanvasDialogue Instance {
        get { return instance; }
    }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}