using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeInside : MonoBehaviour {
    public bool cloakCollected;
    public bool portraitCollected;

    void Update() {
        portraitCollected = GameManager.items[4].collected;
        cloakCollected = GameManager.items[5].collected;
        
        GameManager.firstQuarterCompleted = true;
        Debug.Log(GameManager.firstQuarterCompleted);

        if (!GameManager.secondQuarterCompleted) {
            if (portraitCollected && cloakCollected) {
                ObjectivesManager.Instance.FinishObjective(
                    ObjectivesManager.Instance.FindObjectiveByName("Go \"Home\".")
                );

                ObjectivesManager.Instance.NewObjective(
                    ObjectivesManager.Instance.FindObjectiveByName("Find the Mysterious Man.")
                );
            }
        }
    }
}