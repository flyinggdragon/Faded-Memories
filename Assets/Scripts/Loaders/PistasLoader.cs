using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PistasLoader : MonoBehaviour {
    public ItemLoader.ItemList itemList;

    void Start() {
        itemList = FindObjectOfType<ItemLoader>().itemList;

        foreach (ItemLoader.Item item in itemList.item) {
            Sprite sprite = Resources.Load<Sprite>("Sprites/" + item.fileName);
            
            GameObject obj = new GameObject("Cell " + item.name);
            obj.transform.SetParent(this.transform);
            
            RectTransform rectTransform = obj.AddComponent<RectTransform>();

            rectTransform.anchoredPosition = Vector2.zero; 

            Image image = obj.AddComponent<Image>();
            image.sprite = sprite;

            float width = sprite.rect.width;
            float height = sprite.rect.height;
            float aspectRatio = width / height;

            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.y * aspectRatio, rectTransform.sizeDelta.y);
        }
    }
}