using System.Collections.Generic;
using UnityEngine;

public class Gerald : NPC {
    public override string npcName { get; set; } = "?????";
    public override void RevealName() {
        npcName = "Gerald";
    }

    void Update() {
        if (GameManager.knowsGeraldName) {
            RevealName();
        }      
    }

    public void End() {
        if (!GameManager.talkedToGerald) {
            ObjectivesManager.Instance.FinishObjective(
                ObjectivesManager.Instance.FindObjectiveByName("Discover the meaning of this note.")
            );
        }
        
        GameManager.talkedToGerald = true;
    }
}