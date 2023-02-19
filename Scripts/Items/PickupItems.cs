using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItems : MonoBehaviour
{
    public bool slowPotion, speedPotion, healthPotion, key;
    public Transform player;
    public Inventory inventory;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < 3f)
        {
            if (slowPotion)
            {
                Destroy(gameObject);
                inventory.AddItem("slowPotion");
            }else if (speedPotion)
            {
                Destroy(gameObject);
                inventory.AddItem("speedPotion");
            }
            
        }
    }
}
