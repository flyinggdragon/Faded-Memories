using System.Collections.Generic;
using UnityEngine;

public class Suspect : NPC {
    public override string npcName { get; set; } = "???????";
    public override void RevealName() {
        npcName = "Nimrod Vakjal";
    }

    void Update() {
        if (GameManager.spokeToChief) {
            RevealName();

        }      
    }

    public void TalkToSuspectInPub()
    {
        if (GameManager.spokeToChief)
        {
            GameManager.spokeToNimrod = true;
            ObjectivesManager.Instance.FinishObjective(
                ObjectivesManager.Instance.FindObjectiveByName("Find the suspect.")
            );

            ObjectivesManager.Instance.NewObjective(
                ObjectivesManager.Instance.FindObjectiveByName("Follow the suspect.")
            );
        }
    }

}