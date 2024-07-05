using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultOutside2 : MonoBehaviour {
    GameObject upTrigger;
    Kamu kamu;
    Suspect suspect;

    void Start() {
        upTrigger = GameObject.Find("ExitUp");
        kamu = GameObject.Find("Kamu").GetComponent<Kamu>();
        suspect = GameObject.Find("Suspect").GetComponent<Suspect>();
    }

    void Update() {
        if (!Suspect.enteredCult) {
            kamu.dt.tag = "Untagged";
        } else {
            kamu.dt.tag = "NPC";
        }
    }
}

