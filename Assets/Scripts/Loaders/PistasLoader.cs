using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistasLoader : MonoBehaviour {
    public ItemLoader.ItemList itemList;

    void Start() {
        itemList = FindObjectOfType<ItemLoader>().itemList;

        foreach (ItemLoader.Item item in itemList.item) {
            Sprite sprite = Resources.Load<Sprite>("Sprites/" + item.fileName);

            GameObject obj = new GameObject(item.name);
            SpriteRenderer spriteRenderer = obj.AddComponent<SpriteRenderer>();                // Define o sprite carregado como o sprite do SpriteRenderer
            spriteRenderer.sprite = sprite;
            obj.transform.position = transform.position;
            obj.transform.parent = transform;
        }
    }
}