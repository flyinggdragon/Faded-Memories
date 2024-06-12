using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notebook : MonoBehaviour {
    private static Notebook instance;
    public GameObject NotebookObject;
    public bool isOpen = false;

    /*
    Isso aqui tudo é inútil. Pode-se colocar um caminho direto pro arquivo de áudio
    ao invés de manualmente colocar no Inspector. Também é desnecessário ter
    um outro audioSource aqui, já que a classe AudioManager já cuida disso.
    Vou deixar isso por ora, pois se eu tirar vai quebrar tudo.
    */
    public AudioClip openAudio;
    private AudioSource audioSource;

    // Fim das inutilidades
    private GameObject sentencesContent;
    private GameObject cluesContent;
    private GameObject objectivesContent;
    private Button sentencesButton;
    private Button cluesButton;
    private Button objectivesButton;
    public static Notebook Instance { get; private set; }

    void Awake() {
        if (Instance == null && Instance != this) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else {
            Destroy(gameObject);
        }
    }

    void Start() {
        GameObject.Find("Notebook Holder").SetActive(false);

        sentencesContent = transform.Find("Notebook Back/Sentences Content").gameObject;
        cluesContent = transform.Find("Notebook Back/Clues Content").gameObject;
        objectivesContent = transform.Find("Notebook Back/Objectives Content").gameObject;

        sentencesButton = transform.Find("Notebook Back/Button Holder/Sentences Button").GetComponent<Button>();
        cluesButton = transform.Find("Notebook Back/Button Holder/Clues Button").GetComponent<Button>();
        objectivesButton = transform.Find("Notebook Back/Button Holder/Objectives Button").GetComponent<Button>();
    }

    public void ToggleNotebook() {
        isOpen = !isOpen;
        NotebookObject.SetActive(isOpen);
        Cursor.visible = isOpen;

        if (isOpen) {
            Cursor.lockState = CursorLockMode.None;
        }

        else {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
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