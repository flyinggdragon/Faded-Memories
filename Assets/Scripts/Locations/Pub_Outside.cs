using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pub_Outside : MonoBehaviour {
    void Update() {
        if (GameManager.firstPuzzleCompleted) {
            Exit e = GameObject.Find("UpTrigger").GetComponent<Exit>();

            e.nextLevel = "Pub_Inside_2";
        }
    }
}
