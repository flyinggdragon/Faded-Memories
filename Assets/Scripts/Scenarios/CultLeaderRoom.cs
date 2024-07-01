using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CultLeaderRoom : MonoBehaviour {
    GameObject upTrigger;
    GameObject savil;
    private bool memoryFour = false;

    void Start() {
        upTrigger = GameObject.Find("ExitUp");
        savil = GameObject.Find("Savil");
    }

    void Update() {
        if (
        GameManager.sawBag && GameManager.sawFinances && GameManager.sawConfidentialDocuments
        ) {
            if (!memoryFour) {
                StartCoroutine(
                    BlackScreenText.Instance.CreateBlackScreenWithText(
                        BlackScreenText.Instance.ds4
                    )
                );

                memoryFour = true;
            }

            Destroy(upTrigger);
            savil.SetActive(true);
        }
        
        else {
            savil.SetActive(false);
        }
    }
}