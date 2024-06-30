using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultLeaderRoom : MonoBehaviour {
    GameObject upTrigger;
    GameObject savil;

    void Start() {
        upTrigger = GameObject.Find("ExitUp");
        savil = GameObject.Find("Savil");
    }

    void Update() {
        if (
        GameManager.sawBag && GameManager.sawFinances && GameManager.sawConfidentialDocuments
        ) {
            BlackScreenText.Instance.CreateBlackScreenWithText(BlackScreenText.Instance.ds4);
            Destroy(upTrigger);
            savil.SetActive(true);
        }
        
        else {
            savil.SetActive(false);
        }
    }
}

