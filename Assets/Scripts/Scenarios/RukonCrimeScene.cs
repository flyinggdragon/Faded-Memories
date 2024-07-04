using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RukonCrimeScene : MonoBehaviour
{
    public bool necklaceCollected;

    Exit exitRight;

    void Start() {
        exitRight = GameObject.Find("ExitRight").GetComponent<Exit>();
    }

    void Update() {
        necklaceCollected = GameManager.items[9].collected;

        if (necklaceCollected) {

            exitRight.nextLevel = "Rukon_3";
            
            ObjectivesManager.Objective obj = ObjectivesManager.Instance.FindObjectiveByName("Investigate the murder of Amber Nikolsi.");
        
            if (obj != null) {
                ObjectivesManager.Instance.FinishObjective(obj);
            }

            GameManager.wentToAlleyAndGotNecklace = true;

        }
    }
}

