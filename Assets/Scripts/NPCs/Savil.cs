using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savil : NPC {
    public override string npcName { get; set; } = "??????";
    public override void RevealName() {
        npcName = "Savil Kersiy, The Grand Leader";
        Player.Instance.dialogueManager.UpdateNPCName(npcName);
    }

    [SerializeField] public List<DialogueString> ds = new List<DialogueString>();

    public void SendToJournalist() {
        StartCoroutine(PoliceRaids());
    }

    private IEnumerator PoliceRaids() {
        yield return StartCoroutine(
            BlackScreenText.Instance.CreateBlackScreenWithText(ds)
        );

        LevelManager.Instance.LoadLevel("Last_Scene");
        Player.Instance.transform.position = new Vector3(10.90f, -2.16f, Player.Instance.transform.position.z);
    }
}