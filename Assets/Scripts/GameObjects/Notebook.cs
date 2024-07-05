using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notebook : MonoBehaviour {
    private static Notebook instance;
    public static Notebook Instance { get; private set; }
    public GameObject notebookObject;
    public bool isOpen = false;
    public AudioClip openAudio;
    private AudioSource audioSource;

    // Fim das inutilidades
    private GameObject sentencesContent;
    private GameObject cluesContent;
    private GameObject objectivesContent;
    private Button sentencesButton;
    private Button cluesButton;
    private Button objectivesButton;
    public bool shouldNotClose = false;
    private GameObject dimmedBackground;

    void Awake() {
        if (Instance == null && Instance != this) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {
            Destroy(gameObject);
        }
    }

    public void Debug1() {

        for (int i = 0; i < 6; i++) {
            ObjectivesManager.Instance.FinishObjective(
                GameManager.objectives[i]
            );
        }

        GameManager.knowsPoliceCrackdown = true;
        GameManager.talkedToGerald = true;
        GameManager.knowsGeraldName = true;
        GameManager.firstPuzzleCompleted = true;
        GameManager.sawGraffiti = true;
        GameManager.goingToFirstMeetJacob = true;
        GameManager.talkedToJacob = true;
        GameManager.knowsRumour = true;
        GameManager.firstQuarterCompleted = true;
        GameManager.secondQuarterCompleted = true;
        

        CluesManager.Instance.CollectItem(GameManager.items[2]);
        CluesManager.Instance.CollectItem(GameManager.items[4]);
        CluesManager.Instance.CollectItem(GameManager.items[5]);
    }
    public void Debug2() {
        for (int i = 0; i < 8; i++) {
            ObjectivesManager.Instance.FinishObjective(
                GameManager.objectives[i]
            );
        }

        GameManager.knowsPoliceCrackdown = true;
        GameManager.talkedToGerald = true;
        GameManager.knowsGeraldName = true;
        GameManager.firstPuzzleCompleted = true;
        GameManager.sawGraffiti = true;
        GameManager.goingToFirstMeetJacob = true;
        GameManager.talkedToJacob = true;
        GameManager.knowsRumour = true;
        GameManager.firstQuarterCompleted = true;
        GameManager.secondQuarterCompleted = true;
        GameManager.wentToAlleyAndGotNecklace = true;
        GameManager.sleptDay2 = true;
        GameManager.spokeToNimrod = true;

        CluesManager.Instance.CollectItem(GameManager.items[2]);
        CluesManager.Instance.CollectItem(GameManager.items[4]);
        CluesManager.Instance.CollectItem(GameManager.items[5]);
    }
    public void Debug3() {
        for (int i = 0; i < 10; i++) {
            ObjectivesManager.Instance.FinishObjective(
                GameManager.objectives[i]
            );
        }

        GameManager.knowsPoliceCrackdown = true;
        GameManager.talkedToGerald = true;
        GameManager.knowsGeraldName = true;
        GameManager.firstPuzzleCompleted = true;
        GameManager.sawGraffiti = true;
        GameManager.goingToFirstMeetJacob = true;
        GameManager.talkedToJacob = true;
        GameManager.knowsRumour = true;
        GameManager.firstQuarterCompleted = true;
        GameManager.secondQuarterCompleted = true;
        GameManager.thirdQuarterCompleted = true;
        GameManager.wentToAlleyAndGotNecklace = true;
        GameManager.sleptDay2 = true;
        GameManager.spokeToNimrod = true;
        GameManager.escaping = false;
        GameManager.spokeToHemer = true;
        GameManager.reportedCult = true;/*
        GameManager.raidTime = true;
        GameManager.sawBag = true;
        GameManager.sawFinances = true;*/

        CluesManager.Instance.CollectItem(GameManager.items[2]);
        CluesManager.Instance.CollectItem(GameManager.items[4]);
        CluesManager.Instance.CollectItem(GameManager.items[5]);
        CluesManager.Instance.CollectItem(GameManager.items[11]);
    }

    public void DebugM2() {
        for (int i = 0; i < 5; i++) {
            ObjectivesManager.Instance.FinishObjective(
                GameManager.objectives[i]
            );
        }

        GameManager.knowsPoliceCrackdown = true;
        GameManager.talkedToGerald = true;
        GameManager.knowsGeraldName = true;
        GameManager.firstPuzzleCompleted = true;
        GameManager.sawGraffiti = true;
        GameManager.goingToFirstMeetJacob = true;
        GameManager.talkedToJacob = true;
        GameManager.knowsRumour = true;
        GameManager.firstQuarterCompleted = true;
        GameManager.secondQuarterCompleted = true;

        CluesManager.Instance.CollectItem(GameManager.items[2]);
        CluesManager.Instance.CollectItem(GameManager.items[4]);
        CluesManager.Instance.CollectItem(GameManager.items[5]);
    }

    public void tp() {
        LevelManager.Instance.LoadLevel("Home_Inside_6");
    }

    void Start() {
        notebookObject.SetActive(false);

        dimmedBackground = notebookObject.transform.GetChild(0).gameObject;
        dimmedBackground.SetActive(false);

        sentencesContent = transform.Find("Notebook Back/Sentences Content").gameObject;
        cluesContent = transform.Find("Notebook Back/Clues Content").gameObject;
        objectivesContent = transform.Find("Notebook Back/Objectives Content").gameObject;

        sentencesButton = transform.Find("Notebook Back/Button Holder/Sentences Button").GetComponent<Button>();
        cluesButton = transform.Find("Notebook Back/Button Holder/Clues Button").GetComponent<Button>();
        objectivesButton = transform.Find("Notebook Back/Button Holder/Objectives Button").GetComponent<Button>();
    }

    public IEnumerator ToggleAndLock(int sentenceNumber) {
        ToggleNotebook();
        shouldNotClose = true;

        Transform container = sentencesContent.transform.GetChild(0);
        Transform sentence = container.GetChild(sentenceNumber - 1);
        Transform attention = sentence.GetChild(0);

        attention.gameObject.SetActive(true);
        dimmedBackground.SetActive(true);

        bool AllSpacesFilled(Transform sentence) {
            foreach (Transform child in sentence.transform) {
                SentenceSlots slot = child.GetComponent<SentenceSlots>();
                if (slot == null) {
                    continue;
                }
                
                else {
                    if (!slot.spaceFilled) {
                        return false;
                    }
                }
            }

            return true;
        }

        yield return new WaitUntil(() => AllSpacesFilled(sentence));
        
        shouldNotClose = false;
        attention.gameObject.SetActive(false);
        dimmedBackground.SetActive(false);
        
        ToggleNotebook();
    }

    public void ToggleNotebook() {
        if (shouldNotClose) {
            return;
        }

        isOpen = !isOpen;
        notebookObject.SetActive(isOpen);
        
        AudioManager.Instance.PlaySound(openAudio, 1f);
    }

    public void ActivateCluesContent() {
        AudioManager.Instance.PlaySound(openAudio, 1f);

        DeactivateSentencesContent();
        DeactivateObjectivesContent();
        cluesContent.SetActive(true);
        cluesButton.interactable = false;
    }

    public void DeactivateCluesContent() {
        cluesContent.SetActive(false);
        cluesButton.interactable = true;
    }


    public void ActivateSentencesContent() {
        AudioManager.Instance.PlaySound(openAudio, 1f);

        DeactivateCluesContent();
        DeactivateObjectivesContent();
        sentencesContent.SetActive(true);
        sentencesButton.interactable = false;
    }

    public void DeactivateSentencesContent() {
        sentencesContent.SetActive(false);
        sentencesButton.interactable = true;
    }

    public void ActivateObjectivesContent() {
        AudioManager.Instance.PlaySound(openAudio, 1f);

        DeactivateCluesContent();
        DeactivateSentencesContent();
        objectivesContent.SetActive(true);
        objectivesButton.interactable = false;
    }

    public void DeactivateObjectivesContent() {
        objectivesContent.SetActive(false);
        objectivesButton.interactable = true;
    }
}