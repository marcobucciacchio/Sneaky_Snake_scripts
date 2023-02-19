using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDoor : MonoBehaviour
{
    public GameObject trapDoorUI;
    public Transform player;
    public Transform siblingTrapDoor;
    public GameObject PathGameObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < 2f)
        {
            trapDoorUI.SetActive(true);
            if(Input.GetKeyDown(KeyCode.Space))
            {
                foreach (GameObject enemy in PathGameObject.GetComponent<PoV_node_edges>().enemies)
                {

                    enemy.GetComponent<PathFinding>().chasing = false;
                }
                Score.scoreValue -= 5000;
                player.position = (siblingTrapDoor.position - (Vector3.left *3));
            }
        }
        else
        {
            trapDoorUI.SetActive(false);
        }


      
    }
}
