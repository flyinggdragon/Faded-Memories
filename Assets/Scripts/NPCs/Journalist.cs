using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Journalist : NPC {
    public override string npcName { get; set; } = "Journalist";
    private DialogueTrigger dt;
    [SerializeField] public List<DialogueString> ds = new List<DialogueString>();

    void Start() {
        dt = GetComponentInChildren<DialogueTrigger>();
    }

    public override void RevealName() {}

    void Update() {
        if (!GameManager.firstPuzzleCompleted && !GameManager.sawConfidentialDocuments) { dt.dialogueStrings[9].isEnd = true; }
        else { dt.dialogueStrings[9].isEnd = false; }
    }
    
    public void TellToGoToHall() {
        ObjectivesManager.Objective obj = ObjectivesManager.Instance.FindObjectiveByName("Meet the Unknown Man again.");
        
        if (obj != null) {
            ObjectivesManager.Instance.FinishObjective(obj);
        }

        GameManager.goingToFirstMeetJacob = true;
    }

    public void SendToCredits() {
        StartCoroutine(DisplayFinalText());
    }

    public IEnumerator DisplayFinalText() {
        yield return StartCoroutine(
            BlackScreenText.Instance.CreateBlackScreenWithText(ds)
        );

        MainMenu.comingFromEnd = true;
        AudioManager.Instance.StopBackgroundMusic();
        SceneManager.LoadScene("Main Menu");
    }
}