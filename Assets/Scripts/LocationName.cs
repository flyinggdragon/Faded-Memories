using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LocationName : MonoBehaviour 
{

 public GameObject canvasWindow;
   void Start () 
   {    
        //canvasWindow.sizeDelta = new Vector2(canvasText1.preferredWidth > 200 ? 200 : canvasText1.preferredWidth, canvasText1.preferredHeight);
        Invoke("DisableText", 3.5f);

        RectTransform rt = transform.GetChild(0).GetComponent<RectTransform>();
        rt.anchoredPosition += new Vector2(0, 50);
    }
   void DisableText()
   { 
      canvasWindow.SetActive(false);
   }    
}
