using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hemer : NPC {
    public override string npcName { get; set; } = "Hemer";
    public override void RevealName() {}
    private DialogueTrigger dt;

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }

    void Update() {
        if (GameManager.sawGraffiti) {
            dt.dialogueStrings[30].answerOption2 = "I saw a graffiti in Downtown too...";
            dt.dialogueStrings[30].option2IndexJump = 31;
        } else { 
            dt.dialogueStrings[30].answerOption2 = "These rumours I've heard are come from a anonymous sources";
            dt.dialogueStrings[30].option2IndexJump = 31;
        }
    }

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