using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HoverTipManager1 : MonoBehaviour
{   
    public RectTransform tipWindow1;

    public static Action<CluesManager.Item, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    //public TMP_Text tipItemName;
    //public TMP_Text tipKeyword;
    public TMP_Text tipDescription;

    private void OnEnable()
    {
        OnMouseHover += ShowTip;
        OnMouseLoseFocus += HideTip;
    }

    private void OnDisable()
    {
        OnMouseHover -= ShowTip;
        OnMouseLoseFocus -= HideTip;
    }

    void Start()
    {
        HideTip();
    }

    private void ShowTip(CluesManager.Item tipData, Vector2 mousePos)
    {
        AssignInfo(tipData);
        //tipWindow.sizeDelta = new Vector2(tipItemName.preferredWidth > 200 ? 200 : tipItemName.preferredWidth, tipItemName.preferredHeight);
        //tipWindow.sizeDelta = new Vector2(tipKeyword.preferredWidth > 200 ? 200 : tipKeyword.preferredWidth, tipKeyword.preferredHeight);
        tipWindow1.sizeDelta = new Vector2(tipDescription.preferredWidth > 200 ? 200 : tipDescription.preferredWidth, tipDescription.preferredHeight);
        
        tipWindow1.gameObject.SetActive(true);
        tipWindow1.transform.position = new Vector2(mousePos.x + tipWindow1.sizeDelta.x / 2 +30, mousePos.y);
    }


    private void HideTip()
    {
        tipWindow1.gameObject.SetActive(false);
    }

    private void AssignInfo(CluesManager.Item tipData) {
        if (tipData != null) {
            //tipItemName.text = tipData.itemName;
            //tipKeyword.text = $"Palavra-chave: {tipData.keyword}";
            tipDescription.text = tipData.description;
        }
    }

}
