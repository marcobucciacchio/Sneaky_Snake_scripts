using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Item slowPotion;
    public Item speedPotion;
    public Item healthPotion;

    public Text slowPotionAmount;
    public Text speedPotionAmount;
    public Text healthPotionAmount;

    private void Start()
    {
        slowPotion = new Item(Item.ItemType.SlowPotion, 1);
        speedPotion = new Item(Item.ItemType.SpeedPotion, 1);
        healthPotion = new Item(Item.ItemType.HealthPotion, 1);
    }

    private void Update()
    {
        slowPotionAmount.text = "" + slowPotion.amount;
        speedPotionAmount.text = "" + speedPotion.amount;
        healthPotionAmount.text = " " + healthPotion.amount;
    }

    public void ItemUsed(string s)
    {
        switch (s)
        {
            case "slowPotion":
                if (slowPotion.amount > 0)
                    slowPotion.amount--;
                break;
            case "speedPotion":
                if (speedPotion.amount > 0)
                    speedPotion.amount--;
                break;
            case "healthPotion":
                if (healthPotion.amount > 0)
                    healthPotion.amount--;
                break;
        }
    }

    public void AddItem(string s)
    {
        switch (s)
        {
            case "slowPotion":
                slowPotion.amount++;
                break;
            case "speedPotion":
                speedPotion.amount++;
                break;
            case "healthPotion":
                healthPotion.amount++;
                break;
        }
    }
}
