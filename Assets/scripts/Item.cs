using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public int size;
    public int maxsize;
    public int itemId;
    public static int itemAmount;
    public Sprite Icon;
    public Sprite detailedIcon;
    public static bool consumable = true;
    public void __init__(string itemName, int size, int maxsize, Sprite Icon, Sprite detailedIcon)
    {
        this.itemName = itemName;
        this.size = size;
        this.maxsize = maxsize;
        this.Icon = Icon;
        this.detailedIcon = detailedIcon;
    }
    public void __init__(Item item, int size)
    {
        this.itemName = item.itemName;
        this.size = size;
        this.maxsize = item.maxsize;
        this.Icon = item.Icon;
        this.detailedIcon = item.detailedIcon;
        if(size > maxsize)
        {
            size = maxsize;
        }
    }
    public void __init__(Item item)
    {
        this.name = item.name;
        this.size = item.size;
        this.maxsize = item.maxsize;
        this.Icon = item.Icon;
        this.detailedIcon = item.detailedIcon;
    }
    public Item()
    {
        itemAmount++;
        itemId = itemAmount;
    }
    public static IEnumerator cooldown()
    {
        consumable = false;
        yield return new WaitForSeconds(0.1f);
        consumable = true;
    }
}
