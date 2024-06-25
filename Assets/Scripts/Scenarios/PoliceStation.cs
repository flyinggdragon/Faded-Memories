using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStation : MonoBehaviour {
    Exit upTrigger;
    private bool talkedToDelagete = false;

    void Start() {
        upTrigger = GameObject.Find("UpTrigger_ChiefRoom").GetComponent<Exit>();
    }

    void Update() {
        if (talkedToDelagete) {
            upTrigger.gameObject.SetActive(true);
        } else {
            upTrigger.gameObject.SetActive(false);
        }
    }

    public void BlindEye() {
        talkedToDelagete = true;
    }
}