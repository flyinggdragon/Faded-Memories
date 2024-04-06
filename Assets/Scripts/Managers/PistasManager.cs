using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class PistasManager : MonoBehaviour {
    private ItemLoader.ItemList itemList;
    public ItemLoader.ItemList ItemList {
        get { return itemList; }
    }
    private GameObject cells;

    void Start() {
        itemList = LoadItems();
        GenItemCells();
    }

    void Update() {
        
    }

    private ItemLoader.ItemList LoadItems() {
        string path = "Assets/GameData/ItemList.json";
        string jsonContent = File.ReadAllText(path);
        
        return JsonUtility.FromJson<ItemLoader.ItemList>(jsonContent);
    }

    private void GenItemCells() {
        cells = this.transform.GetChild(0).gameObject;

        foreach (ItemLoader.Item item in itemList.item) {
            item.Collected = true;

            GameObject cell = new GameObject("Cell " + item.itemName);
            cell.transform.SetParent(cells.transform);
            cell.gameObject.SetActive(item.Collected);
            
            RectTransform rectTransform = cell.AddComponent<RectTransform>();

            rectTransform.anchoredPosition = Vector2.zero; 

            Sprite sprite = Resources.Load<Sprite>("Sprites/" + item.fileName);
            Image image = cell.AddComponent<Image>();
            image.sprite = sprite;

            float width = sprite.rect.width;
            float height = sprite.rect.height;
            float aspectRatio = width / height;

            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.y * aspectRatio, rectTransform.sizeDelta.y);
            
            item.Obj = cell;
        }
    }
}