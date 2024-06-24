using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;


public class HoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{   
    public CluesManager.Item tipData;

    public void RetrieveData(CluesManager.Item tip) {
        tipData = tip;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowMessage(tipData);
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoverTipManager.OnMouseLoseFocus();
    }

    private void ShowMessage(CluesManager.Item tipData)
    {
        HoverTipManager.OnMouseHover(tipData, Input.mousePosition);
    }
}
