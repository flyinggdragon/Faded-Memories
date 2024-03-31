using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notebook : MonoBehaviour {
    public GameObject NotebookObject;
    private bool isOpen = false;
    private DialogueManager dialogueManager;
    public AudioClip openAudio;
    private AudioSource audioSource;

    void Start() {
        dialogueManager = GameObject.Find("Player").GetComponent<DialogueManager>();
        
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = openAudio;
    }

    public void ToggleNotebook() {
        if (dialogueManager.IsDialoguing) { return; }

        if (!isOpen) { 
            NotebookObject.SetActive(true);
            isOpen = true;
            
            PlayOpenSound();
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

        else {
            PlayOpenSound();

            NotebookObject.SetActive(false);
            isOpen = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void PlayOpenSound() {
        audioSource.Play();
    }
}