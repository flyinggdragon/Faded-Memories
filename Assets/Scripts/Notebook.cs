using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Start() {
        dialogueManager = GameObject.Find("Player").GetComponent<DialogueManager>();
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = openAudio;

        audioManager = GameObject.FindObjectOfType<AudioManager>();
        if (audioManager == null) {
            Debug.LogError("AudioManager not found in the scene.");
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
}