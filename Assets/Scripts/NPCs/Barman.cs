using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barman : NPC {
    public override string npcName { get; set; } = "Barman";

    public override void Initialize() {}
    
    public override void Reset() {}
    private bool paidFirstTime = false;

    public void Pay(int value) {
        GameManager.money -= value;

        if (!paidFirstTime) {
            ObjectivesManager.Instance.FinishObjective(
                ObjectivesManager.Instance.FindObjectiveByName("Comer alguma coisa")
            );
            
            paidFirstTime = true;
        } else { return; }
    }
}