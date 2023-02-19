using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        SlowPotion,
        SpeedPotion,
        HealthPotion,
        Key,
    }

    public ItemType itemType;
    public int amount;

    public Item(ItemType i, int a)
    {
        itemType = i;
        amount = a;
    }

}
