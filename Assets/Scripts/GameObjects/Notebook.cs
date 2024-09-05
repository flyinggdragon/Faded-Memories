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

    public GameObject sentencesContent;
    public GameObject cluesContent;
    public GameObject objectivesContent;
    public Button sentencesButton;
    public Button cluesButton;
    public Button objectivesButton;
    public bool shouldNotClose = false;
    public GameObject dimmedBackground;

    void Awake() {
        if (Instance == null && Instance != this) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {
            Destroy(gameObject);
        }
    }

    public IEnumerator ToggleAndLock(int sentenceNumber) {
        ToggleNotebook();
        shouldNotClose = true;

        Transform container = sentencesContent.transform.GetChild(0);
        Transform sentence = container.GetChild(sentenceNumber - 1);
        Transform attention = sentence.GetChild(0);

        attention.gameObject.SetActive(true);
        dimmedBackground.SetActive(true);

        ActivateCluesContent();

        yield return new WaitUntil(() => AllSpacesFilled(sentence));
        
        shouldNotClose = false;
        attention.gameObject.SetActive(false);
        dimmedBackground.SetActive(false);
        
        ToggleNotebook();

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
    }

    public void ToggleNotebook() {
        if (shouldNotClose) {
            return;
        }

        isOpen = !isOpen;

        Player.Instance.shouldMove = !isOpen;
        Player.Instance.shouldDialogue = !isOpen;
        Player.Instance.shouldPause = !isOpen;
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