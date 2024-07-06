using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoliceStation : MonoBehaviour {
    Exit upTrigger;
    public static bool blindEye = false;
    public static bool allowedEntrance = false;
    public GameObject officer;

    void Start() {
        if (GameObject.Find("UpTrigger_ChiefRoom").GetComponent<Exit>() != null) {
            upTrigger = GameObject.Find("UpTrigger_ChiefRoom").GetComponent<Exit>();
        }
    }

    void Update() {
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