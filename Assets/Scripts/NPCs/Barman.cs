using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barman : NPC {
    public override string npcName { get; set; } = "Barman";
    private bool paidFirstTime = false;
    public override void RevealName() {}

    public void Pay(int value) {
        GameManager.money -= value;

        if (!paidFirstTime) {
            ObjectivesManager.Objective obj = ObjectivesManager.Instance.FindObjectiveByName("Eat something.");
            ObjectivesManager.Instance.FinishObjective(obj);
            
            paidFirstTime = true;
        } else { return; }
    }
}