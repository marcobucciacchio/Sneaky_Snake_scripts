    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingSystem : MonoBehaviour
{
    public Rigidbody slowPotionPrefab;
    public GameObject cursor;
    public LayerMask layer;
    public Transform shootPoint;

    private SpriteRenderer targetColor;

    public bool itemSelected, slowPotionSelected;

    public Inventory inventory;

    private Camera cam;

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        cam = Camera.main;
        itemSelected = false;

        targetColor = cursor.GetComponent<SpriteRenderer>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        
    }
     
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
            SelectItem("SlowPotion");
        
        if(Input.GetKeyDown(KeyCode.Alpha2))
            SelectItem("SpeedPotion");
        
        if(Input.GetKeyDown(KeyCode.Alpha3))
            SelectItem("HealthPotion");
            
        if(itemSelected)
            ThrowItem();
    }

    void ThrowItem()
    {
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetMouseButtonDown(1) && slowPotionSelected)
        {
            itemSelected = false;
            slowPotionSelected = false;
        }
        
        if (Physics.Raycast(cameraRay, out hit, 100f, layer) && itemSelected)
        {
            targetColor.material.color = new Color(255f/255f, 117f/255f, 26f/255f);
            cursor.SetActive(true);
            cursor.transform.position = hit.point + Vector3.up * 0.1f;
            
            Vector3 velocityInitial = CalculateVelocity(hit.point, shootPoint.position, 1f);

            if (Input.GetMouseButtonDown(0))
            {
                
                itemSelected = false;
                cursor.SetActive(false);
                if (slowPotionSelected)
                {
                    Rigidbody obj = Instantiate(slowPotionPrefab, shootPoint.position, Quaternion.identity);
                    obj.velocity = velocityInitial;
                    inventory.ItemUsed("slowPotion");
                    slowPotionSelected = !slowPotionSelected;
                }

            }
        }
        else if (!itemSelected)
        {
             cursor.SetActive(false);
        }
        else
        {
            targetColor.material.color = new Color(172f/255f, 0f/255f, 230f/255f);
        }
    }
    
    Vector3 CalculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0.0f;

        float positionY = distance.y;
        float positionX = distanceXZ.magnitude;

        float velocityXZ = positionX / time;
        float velocityY = positionY / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= velocityXZ;
        result.y = velocityY;

        return result;
    }

    public void SelectItem(string itemName)
    {
        if (itemName == "SlowPotion" && inventory.slowPotion.amount > 0)
        { 
            slowPotionSelected = true;
            itemSelected = true;
        }

        if (itemName == "SpeedPotion" && inventory.speedPotion.amount > 0)
        {
            //add player speed-up function then access it

            player.GetComponent<PlayerMovement>().speedUpPlayer();
            inventory.ItemUsed("speedPotion");
        }
        
        if (itemName == "HealthPotion" && inventory.healthPotion.amount > 0)
        {
            FindObjectOfType<HealthBar>().UsePotion();
            inventory.ItemUsed("healthPotion");
        }
    }
}
