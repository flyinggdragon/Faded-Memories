using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DowntownHigh : MonoBehaviour
{
    Exit upTriggerRukon;

    void Start() {
        upTriggerRukon = GameObject.Find("UpTrigger_Rukon").GetComponent<Exit>();
    }

    void Update() {
        if (GameManager.secondQuarterCompleted) {
            upTriggerRukon.nextLevel = "Rukon_2";
        } else {
            upTriggerRukon.nextLevel = "Rukon";
        }
    }
}

