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
                    StartCoroutine(CompleteMission());

                    memoryOne = true;
                }
            }
        }
    }

    private IEnumerator CompleteMission() {
        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Go \"Home\".")
        );

        yield return StartCoroutine(Notebook.Instance.ToggleAndLock(1));

        StartCoroutine(
            BlackScreenText.Instance.CreateBlackScreenWithText(
                BlackScreenText.Instance.ds1
            )
        );
    }
}