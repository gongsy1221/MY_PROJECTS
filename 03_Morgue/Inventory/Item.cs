using UnityEngine;

public enum ItemType
{
    
}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public ItemType itemType;
    public string itemName;
    public Sprite icon;
}