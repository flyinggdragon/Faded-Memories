using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeInside : MonoBehaviour {
    public bool cloakCollected;
    public bool portraitCollected;

    void Update() {
        portraitCollected = GameManager.items[4].collected;
        cloakCollected = GameManager.items[5].collected;
        

        if (!GameManager.secondQuarterCompleted) {
            if (portraitCollected && cloakCollected) {
                GameManager.firstQuarterCompleted = true;
                
                ObjectivesManager.Instance.FinishObjective(
                    ObjectivesManager.Instance.FindObjectiveByName("Go \"Home\".")
                );

                BlackScreenText.Instance.CreateBlackScreenWithText(BlackScreenText.Instance.ds1);
            }
        }
    }
}