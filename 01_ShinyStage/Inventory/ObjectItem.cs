using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectItem : MonoBehaviour, IObjectItem
{
    [Header("Item")]
    public Item item;

    [Header("Item Image")]
    public SpriteRenderer itemImgae;

    private void Start()
    {
        itemImgae.sprite = item.itemImage;
    }

    public Item ClickItem()
    {
        return this.item;
    }
}
