using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PubOutside : MonoBehaviour {
    Exit upTrigger;

    void Start() {
        upTrigger = GameObject.Find("UpTrigger").GetComponent<Exit>();
    }

    void Update() {
        if (GameManager.firstPuzzleCompleted) {
            upTrigger.nextLevel = "Pub_Inside_2";
        }

        if (GameManager.talkedToSuspectInRukon) {
            upTrigger.nextLevel = "Pub_Inside_3";
        }

        if (GameManager.thirdQuarterCompleted) {   
            upTrigger.nextLevel = "Pub_Inside_4";
        }
    }
}