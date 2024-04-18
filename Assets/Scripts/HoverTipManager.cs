using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HoverTipManager : MonoBehaviour
{   

    public TextMeshProUGUI tipText;
    public Image tipImage;
    public RectTransform tipWindow;

    public static Action<string, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    private void OnEnable()
    {
        OnMouseHover += ShowTip;
        //OnMouseHover += ShowImage;
        OnMouseLoseFocus += HideTip;
        //OnMouseLoseFocus += HideImage;
        
    }

    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        //OnMouseHover -= ShowImage;
        OnMouseLoseFocus -= HideTip;
        //OnMouseLoseFocus -= HideImage;
    }

    void Start()
    {
        HideTip();
    }

    private void ShowTip(string tip, Vector2 mousePos)
    {
        tipText.text = tip;
        tipWindow.sizeDelta = new Vector2(tipText.preferredWidth > 200 ? 200 : tipText.preferredWidth, tipText.preferredHeight);
        
        tipWindow.gameObject.SetActive(true);
        tipWindow.transform.position = new Vector2((mousePos.x + tipWindow.sizeDelta.x / 2) +10, mousePos.y);
    }

   // private void ShowImage(string img, Vector2 mousePos)
   // {
    //    tipImage.sprite = Resources.Load<Sprite>(img);
      //  tipWindow.sizeDelta = new Vector2(tipImage.preferredWidth > 100 ? 100 : tipImage.preferredWidth, tipImage.preferredHeight);

        //tipWindow.gameObject.SetActive(true);
        //tipWindow.transform.position = new Vector2((mousePos.x + tipWindow.sizeDelta.x /2) +10, mousePos.y);
    //}

    private void HideTip()
    {
        tipText.text = default;
        tipWindow.gameObject.SetActive(false);
    }

   // private void HideImage()
    //{
     //   tipText.text = default;
      //  tipWindow.gameObject.SetActive(false);
    //}


}
