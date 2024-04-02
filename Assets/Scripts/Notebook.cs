using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notebook : MonoBehaviour {
    public GameObject NotebookObject;
    private bool isOpen = false;
    private DialogueManager dialogueManager;

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
    private GameObject frasesContent;
    private GameObject pistasContent;
    private Button frasesButton;
    private Button pistasButton;

    void Start() {
        dialogueManager = GameObject.Find("Player").GetComponent<DialogueManager>();
        
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager == null) {
            Debug.LogError("AudioManager not found in the scene.");
        }

        frasesContent = transform.Find("Notebook Back/FrasesContent").gameObject;
        pistasContent = transform.Find("Notebook Back/PistasContent").gameObject;
        
        if (frasesContent == null) {
            Debug.LogError("Frases Content not found!");
        }
        if (pistasContent == null) {
            Debug.LogError("Pistas Content not found!");
        }

        frasesButton = transform.Find("Notebook Back/Button Holder/FrasesButton").GetComponent<Button>();
        pistasButton = transform.Find("Notebook Back/Button Holder/PistasButton").GetComponent<Button>();

        if (frasesButton == null) {
            Debug.LogError("Frases Button not found!");
        }
        if (pistasButton == null) {
            Debug.LogError("Pistas Button not found!");
        }
    }

    public void ToggleNotebook() {
        if (dialogueManager.IsDialoguing) { return; }

        if (!isOpen) { 
            NotebookObject.SetActive(true);
            isOpen = true;
            
            audioManager.PlaySound(openAudio);
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else {
            NotebookObject.SetActive(false);
            isOpen = false;

            audioManager.PlaySound(openAudio);

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void ActivatePistasContent() {
        audioManager.PlaySound(openAudio);

        frasesContent.SetActive(false);
        frasesButton.interactable = true;
        pistasContent.SetActive(true);
        pistasButton.interactable = false;
    }

    public void ActivateFrasesContent() {
        audioManager.PlaySound(openAudio);

        pistasContent.SetActive(false);
        pistasButton.interactable = true;
        frasesContent.SetActive(true);
        frasesButton.interactable = false;
    }
}