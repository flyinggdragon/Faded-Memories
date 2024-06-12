using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HoverTipManager : MonoBehaviour
{   
    public RectTransform tipWindow;

    public static Action<CluesManager.Item, Vector2> OnMouseHover;
    public static Action OnMouseLoseFocus;

    public TMP_Text tipItemName;
    public TMP_Text tipKeyword;
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
        tipWindow.sizeDelta = new Vector2(tipItemName.preferredWidth > 200 ? 200 : tipItemName.preferredWidth, tipItemName.preferredHeight);
        
        tipWindow.gameObject.SetActive(true);
        tipWindow.transform.position = new Vector2(mousePos.x + tipWindow.sizeDelta.x / 2 +15, mousePos.y);
    }


    private void HideTip()
    {
        tipWindow.gameObject.SetActive(false);
    }

    private void AssignInfo(CluesManager.Item tipData) {
        if (tipData != null) {
            tipItemName.text = tipData.itemName;
            tipKeyword.text = $"Palavra-chave: {tipData.keyword}";
            tipDescription.text = tipData.description;
        }
    }

}
