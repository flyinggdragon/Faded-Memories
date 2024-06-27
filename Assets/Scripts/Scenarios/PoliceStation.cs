using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStation : MonoBehaviour {
    Exit upTrigger;
    private bool talkedToDelegete = false;

    void Start() {
        upTrigger = GameObject.Find("UpTrigger_ChiefRoom").GetComponent<Exit>();
    }

    void Update() {
        if (!talkedToDelegete) {
            upTrigger.gameObject.SetActive(true);
        } else {
            upTrigger.gameObject.SetActive(false);
        }

        if (GameManager.wentToAlleyAndGotNecklace) {
            upTrigger.nextLevel = "PoliceStation_ChiefRoom_2";
        }
    }

    public void BlindEye() {
        talkedToDelegete = true;
    }
}