using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeInside : MonoBehaviour {
    public bool cloakCollected;
    public bool portraitCollected;
    private bool memoryOne = false;

    void Update() {
        portraitCollected = GameManager.items[4].collected;
        cloakCollected = GameManager.items[5].collected;
        

        if (!GameManager.secondQuarterCompleted) {
            if (portraitCollected && cloakCollected) {
                GameManager.firstQuarterCompleted = true;

                if (!memoryOne) {
                    CompleteMission();

                    memoryOne = true;
                }
            }
        }
    }

    private void CompleteMission() {
        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Go \"Home\".")
        );

        StartCoroutine(
            BlackScreenText.Instance.CreateBlackScreenWithText(
                BlackScreenText.Instance.ds1
            )
        );
    }
}