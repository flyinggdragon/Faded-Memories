using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultOutside3 : MonoBehaviour {
    GameObject upTrigger;

    void Start() {
        upTrigger = GameObject.Find("ExitUp");
    }

    void Update() {
        upTrigger.SetActive(Cultist.allowedEntrance);
    }
}

