using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hemer : NPC {
    public override string npcName { get; set; } = "Hemer";
    public override void RevealName() {}

    public void GiveBusinessCard() {
        StartCoroutine(ShowBusiness());
        
        GameManager.spokeToHemer = true;
    }

    private IEnumerator ShowBusiness() {
        yield return StartCoroutine(
            BlackScreenText.Instance.CreateTransparentItemDisplayer(
                BlackScreenText.Instance.hemerBussinessCard
            )
        );
        
        if (!GameManager.items[11].collected) { 
            CluesManager.Instance.CollectItem(
                GameManager.items[11]
            );
        }
    }
}