using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/New Item", order = 0)]
public class Item : ScriptableObject
{
    /// <summary>
    /// The name of the item.
    /// </summary>
    public string itemName;
    /// <summary>
    /// The item's price.
    /// </summary>
    public float price;
    /// <summary>
    /// How care the item is.
    /// </summary>
    public Rarity rarity;
    /// <summary>
    /// An icon for the inventory UI.
    /// </summary>
    public Sprite icon;
}

/// <summary>
/// Item rarity.
/// </summary>
public enum Rarity
{
    Normal,
    Rare,
    Legendary
}