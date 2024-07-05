using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour {
    
    // Gambiarra
    public static bool firstTime = true;
    void Start() {
        if (firstTime) { 
            Notebook.Instance.ToggleNotebook();
        }
    }
}
