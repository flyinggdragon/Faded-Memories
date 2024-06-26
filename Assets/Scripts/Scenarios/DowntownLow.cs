using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DowntownLow : MonoBehaviour
{
    Exit upTriggerCult;

    void Start() {
        upTriggerCult = GameObject.Find("UpTrigger_Cult").GetComponent<Exit>();
    }

    void Update() {
        if (GameManager.spokeToNimrod) {
            upTriggerCult.nextLevel = "CultHQ_Outside_2";
        }
    }
}
