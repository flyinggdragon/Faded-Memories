using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notebook : MonoBehaviour {
    private static Notebook instance;
    private ElementContainer elementContainer;
    public GameObject NotebookObject;
    private bool isOpen = false;

    public bool IsOpen {
        get { return isOpen; }
    }

    /*
    Isso aqui tudo é inútil. Pode-se colocar um caminho direto pro arquivo de áudio
    ao invés de manualmente colocar no Inspector. Também é desnecessário ter
    um outro audioSource aqui, já que a classe AudioManager já cuida disso.
    Vou deixar isso por ora, pois se eu tirar vai quebrar tudo.
    */
    public AudioClip openAudio;
    private AudioSource audioSource;

    // Fim das inutilidades
    private AudioManager audioManager;
    private GameObject sentencesContent;
    private GameObject cluesContent;
    private Button sentencesButton;
    private Button cluesButton;

    public static Notebook Instance {
        get {
            return instance;
        }
    }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        GameObject.Find("Notebook Holder").SetActive(false);

        elementContainer = GameObject.Find("Element Container").GetComponent<ElementContainer>();
        audioManager = elementContainer.audioManager;

        sentencesContent = transform.Find("Notebook Back/Sentences Content").gameObject;
        cluesContent = transform.Find("Notebook Back/Clues Content").gameObject;

        sentencesButton = transform.Find("Notebook Back/Button Holder/Sentences Button").GetComponent<Button>();
        cluesButton = transform.Find("Notebook Back/Button Holder/Clues Button").GetComponent<Button>();
    
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
        
        audioManager.PlaySound(openAudio, 1f);
    }

    public void ActivateCluesContent() {
        audioManager.PlaySound(openAudio, 1f);

        sentencesContent.SetActive(false);
        sentencesButton.interactable = true;
        cluesContent.SetActive(true);
        cluesButton.interactable = false;
    }

    public void ActivateSentencesContent() {
        audioManager.PlaySound(openAudio, 1f);

        cluesContent.SetActive(false);
        cluesButton.interactable = true;
        sentencesContent.SetActive(true);
        sentencesButton.interactable = false;
    }
}