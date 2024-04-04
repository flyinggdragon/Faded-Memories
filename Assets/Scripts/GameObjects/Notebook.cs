using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Notebook : MonoBehaviour {
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
    private GameObject frasesContent;
    private GameObject pistasContent;
    private Button frasesButton;
    private Button pistasButton;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();

        frasesContent = transform.Find("Notebook Back/FrasesContent").gameObject;
        pistasContent = transform.Find("Notebook Back/PistasContent").gameObject;

        frasesButton = transform.Find("Notebook Back/Button Holder/FrasesButton").GetComponent<Button>();
        pistasButton = transform.Find("Notebook Back/Button Holder/PistasButton").GetComponent<Button>();
    }

    public void ToggleNotebook() {
        // Abre
        if (!isOpen) {
            NotebookObject.SetActive(true);
            isOpen = true;
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Fecha
        else {
            NotebookObject.SetActive(false);
            isOpen = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
        audioManager.PlaySound(openAudio);
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