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
        
        CluesManager.Instance.CollectItem(
            CluesManager.Instance.FindItem("Business Card")
        );
    }
}