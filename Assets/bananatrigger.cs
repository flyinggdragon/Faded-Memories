using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bananatrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {  
        if (other.CompareTag("Player"))
        {
            CreateToastModal();
            Debug.Log("Teste Teste Teste");
        }
        
    }

    public void CreateToastModal()
    {
        ToastModalWindow.Create(ignorable: true)
                        .SetHeader("Você pisou na bunana!")
                        .SetBody("Teve sorte de não ter escorregado!")
                        .SetDelay(3f) // Set it to 0 to make popup persistent
                        //.SetIcon(sprite) // Also you can set icon
                        .Show();
    }



}





