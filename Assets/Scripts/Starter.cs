using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starter : MonoBehaviour {
    
    // Temporário (e gambiarra).
    public static bool firstTime = true;
    void Start() {
        if (firstTime) { GameManager.Initialize(); }
    }
}
