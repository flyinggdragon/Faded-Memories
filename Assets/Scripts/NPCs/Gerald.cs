using System.Collections.Generic;
using UnityEngine;

public class Gerald : NPC {
    public override string npcName { get; set; } = "?????";

    void Update() {
        if (GameManager.knowsGeraldName) {
            npcName = "Gerald";
        }      
    }

    public void End() {
        if (!GameManager.talkedToGerald) {
            ObjectivesManager.Instance.FinishObjective(
                ObjectivesManager.Instance.FindObjectiveByName("Find this note's purpose.")
            );
        }
        
        GameManager.talkedToGerald = true;
    }
}