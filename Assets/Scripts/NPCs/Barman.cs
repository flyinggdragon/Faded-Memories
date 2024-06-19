using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barman : NPC {
    public override string npcName { get; set; } = "Barman";
    private bool paidFirstTime = false;

    public void Pay(int value) {
        GameManager.money -= value;

        if (!paidFirstTime) {
            ObjectivesManager.Instance.FinishObjective(
                ObjectivesManager.Instance.FindObjectiveByName("Eat something.")
            );
            
            paidFirstTime = true;
        } else { return; }
    }
}