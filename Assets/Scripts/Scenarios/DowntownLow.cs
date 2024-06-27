using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DowntownLow : MonoBehaviour
{
    Exit upTriggerCult;
    Exit upTriggerPoliceStation;

    void Start() {
        upTriggerCult = GameObject.Find("UpTrigger_Cult").GetComponent<Exit>();
        upTriggerPoliceStation = GameObject.Find("UpTrigger_PoliceStation").GetComponent<Exit>();
    }

    void Update() {
        if (GameManager.spokeToNimrod) {
            upTriggerCult.nextLevel = "CultHQ_Outside_2";
        }

        if (GameManager.raidTime) {
            upTriggerCult.nextLevel = "CultHQ_Outside_3";
        }

        if (GameManager.spokeToHemer) {
            upTriggerPoliceStation.nextLevel = "PoliceStation_Main_2";
        }
    }
}
