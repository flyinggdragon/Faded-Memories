using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStation : MonoBehaviour {
    Exit upTrigger;
    public static bool blindEye = false;
    public static bool allowedEntrance = false;

    void Start() {
        if (GameObject.Find("UpTrigger_ChiefRoom").GetComponent<Exit>() != null) {
            upTrigger = GameObject.Find("UpTrigger_ChiefRoom").GetComponent<Exit>();
        }
    }

    void Update() {
        Debug.Log("be: " + blindEye);
        Debug.Log("ae: " + allowedEntrance);
        Debug.Log("upn: " + upTrigger != null);
        
        if ((blindEye || allowedEntrance) && (upTrigger != null)) {
            upTrigger.gameObject.SetActive(true);
        } else {
            upTrigger.gameObject.SetActive(false);
        }
    }

    public void AllowEntrance() {
        allowedEntrance = true;
    }

    public void BlindEye() {
        blindEye = true;
    }
}