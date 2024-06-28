using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultCorridor : MonoBehaviour {
    Exit rightTrigger;

    void Start() {
        rightTrigger = GameObject.Find("ExitRight").GetComponent<Exit>();
    }

    void Update() {
        if (GameManager.sawBag && GameManager.sawFinances) {
            rightTrigger.nextLevel = "CultHQ_LeaderRoom";
        }
    }
}

