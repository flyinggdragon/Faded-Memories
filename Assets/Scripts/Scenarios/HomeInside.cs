using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeInside : MonoBehaviour {
    void Update() {
        if (GameManager.firstTimeHome) {
            ObjectivesManager.Instance.FinishObjective(
                ObjectivesManager.Instance.FindObjectiveByName("Go \"Home\".")
            );

            GameManager.firstTimeHome = false;
        }
    }
}