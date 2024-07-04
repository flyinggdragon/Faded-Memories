using System.Collections.Generic;
using UnityEngine;

public class Suspect : NPC {
    public override string npcName { get; set; } = "???????";
    public override void RevealName() {
        npcName = "Nimrod Vakjal";
        Player.Instance.dialogueManager.UpdateNPCName(npcName);
    }
    public bool enteredCult = false;

    public void TalkToSuspectInRukon() {
        GameManager.talkedToSuspectInRukon = true;
    }

    public void TalkToSuspectInPub() {
        GameManager.spokeToNimrod = true;
        ObjectivesManager.Objective obj = ObjectivesManager.Instance.FindObjectiveByName("Find the suspect.");
        
        if (obj != null) {
            ObjectivesManager.Instance.FinishObjective(obj);
        }
    }

    public void EnterCult() {
        enteredCult = true;
        Destroy(gameObject);
    }
}