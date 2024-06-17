using System.Collections.Generic;
using UnityEngine;

public class Gerald : NPC {
    public override string npcName { get; set; } = "?????";

    public override void Initialize() {}

    public override void Reset() {}

    void Update() {
        if (GameManager.knowsGeraldName) {
            npcName = "Gerald";
        }      
    }

    public void End() {
        if (!GameManager.talkedToGerald) {
            ObjectivesManager.Instance.FinishObjective(
                ObjectivesManager.Instance.FindObjectiveByName("Achar a utilidade deste bilhete")
            );
        }
        
        GameManager.talkedToGerald = true;
    }
}