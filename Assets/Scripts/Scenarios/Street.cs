using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Street : MonoBehaviour {
    Exit upTrigger;

    void Start() {
        upTrigger = GameObject.Find("ExitUp_Hall").GetComponent<Exit>();
    }

    void Update() {
        if (GameManager.goingToFirstMeetJacob) {
            upTrigger.nextLevel = "Hall";
        } else { upTrigger.nextLevel = "Hall_2"; }
    }
}