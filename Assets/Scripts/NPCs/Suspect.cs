using System.Collections.Generic;
using UnityEngine;

public class Suspect : NPC {
    public override string npcName { get; set; } = "???????";
    public override void RevealName() {
        npcName = "Nimrod Vakjal";
    }

    public void TalkToSuspectInRukon() {
        GameManager.talkedToSuspectInRukon = true;
    }

    public void TalkToSuspectInPub() {
        GameManager.spokeToNimrod = true;
        ObjectivesManager.Instance.FinishObjective(
            ObjectivesManager.Instance.FindObjectiveByName("Find the suspect.")
        );
    }
}