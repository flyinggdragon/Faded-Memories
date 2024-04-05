using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; // Adicione este using para usar o método FirstOrDefault

public class PistasManager : MonoBehaviour {
    private ItemLoader.ItemList itemList;
    public ItemLoader.ItemList ItemList {
        get { return itemList; }
    }
    private GameObject cells;

    void Start() {
        itemList = FindObjectOfType<ItemLoader>().itemList;

        GenItemCells();
    }

    void Update() {
        // Muito ineficiente, mas funciona. Por ora deixa assim.
        // Classe Item pra cada instância dessa deve arrumar.
        foreach (Transform cell in cells.transform) {
            string itemName = cell.gameObject.name.Replace("Cell ", "");
            ItemLoader.Item item = itemList.item.ToList().FirstOrDefault(x => x.name == itemName);
            
            cell.gameObject.SetActive(item.collected);
            
        }
    }

    private void GenItemCells() {
        cells = this.transform.GetChild(0).gameObject;

        foreach (ItemLoader.Item item in itemList.item) {
            item.collected = false;

            GameObject cell = new GameObject("Cell " + item.name);
            cell.transform.SetParent(cells.transform);
            cell.gameObject.SetActive(item.collected);
            
            RectTransform rectTransform = cell.AddComponent<RectTransform>();

            rectTransform.anchoredPosition = Vector2.zero; 

            Sprite sprite = Resources.Load<Sprite>("Sprites/" + item.fileName);
            Image image = cell.AddComponent<Image>();
            image.sprite = sprite;

            float width = sprite.rect.width;
            float height = sprite.rect.height;
            float aspectRatio = width / height;

            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.y * aspectRatio, rectTransform.sizeDelta.y);
        }
    }
}