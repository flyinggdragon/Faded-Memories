using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notebook : MonoBehaviour {
    public GameObject NotebookObject;
    private bool isOpen = false;
    private DialogueManager dialogueManager;

    void Start() {
        dialogueManager = GameObject.Find("Player").GetComponent<DialogueManager>();
    }

    public void ToggleNotebook() {
        if (dialogueManager.IsDialoguing) { return; }

        if (!isOpen) { 
            NotebookObject.SetActive(true);
            isOpen = true;
            
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else {
            NotebookObject.SetActive(false);
            isOpen = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
