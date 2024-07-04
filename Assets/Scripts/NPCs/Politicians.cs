using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Politicians : NPC {
    public override string npcName { get; set; } = "Group of Politicians";
    public override void RevealName() {}
    [SerializeField] public List<DialogueString> ds = new List<DialogueString>();
    private DialogueTrigger dt;

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }

    void Update() {
        if (!GameManager.knowsRumour) {
            dt.dialogueStrings[0].isEnd = true;
            dt.dialogueStrings[0].isQuestion = false;
        } else {
            dt.dialogueStrings[0].isEnd = false;
            dt.dialogueStrings[0].isQuestion = true;
        }
    }

    public void DropPatch() {
        if (!GameManager.items[6].collected) { 
            CluesManager.Instance.CollectItem(
                GameManager.items[6]
            );
        }
    }

    public void CallSecurity() {
        StartCoroutine(ShowText());

        DropPatch();
        
    }

    private IEnumerator ShowText() {
        yield return StartCoroutine(
            BlackScreenText.Instance.CreateBlackScreenWithText(ds)
        );

        LevelManager.Instance.LoadLevel("Home_Inside_2");
    }
}