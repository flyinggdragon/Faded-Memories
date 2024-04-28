using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class monalisastart : MonoBehaviour
{   
    private bool triggerin;

    private void Update() 
    {
        if (triggerin && Input.GetKeyDown(KeyCode.E)) 
        {
            SceneManager.LoadScene("Puzzle");
            Cursor.lockState = CursorLockMode.None;
        }
    }
 
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            triggerin = true;
        }
    }
 
    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Player")) 
        {
            triggerin = false;
        }
    }



    

}
