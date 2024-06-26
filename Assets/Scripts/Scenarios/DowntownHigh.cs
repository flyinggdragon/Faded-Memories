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
        if (GameManager.talkedToJacob) {
            upTriggerRukon.nextLevel = "Rukon_2";
        }
    }
}

