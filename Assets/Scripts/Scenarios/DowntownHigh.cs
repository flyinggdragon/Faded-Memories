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
        if (GameManager.thirdQuarterCompleted) {
            upTriggerRukon.nextLevel = "Rukon";
        }

        else if (GameManager.secondQuarterCompleted) {
            if (GameManager.wentToAlleyAndGotNecklace) {
                if (!GameManager.talkedToSuspectInRukon) {
                    upTriggerRukon.nextLevel = "Rukon_3";
                } else {
                    upTriggerRukon.nextLevel = "Rukon_2";
                }
            } else {
                upTriggerRukon.nextLevel = "Rukon_2";
            }
        }

        else {
            upTriggerRukon.nextLevel = "Rukon";
        }
    }
}

