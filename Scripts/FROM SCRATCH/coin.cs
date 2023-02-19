using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    [SerializeField]
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if(distance < 2f)
        {
            if (gameObject.tag.Equals("small"))
            {
                Destroy(gameObject);
                Score.scoreValue += 100;
            }else if (gameObject.tag.Equals("medium"))
            {
                Destroy(gameObject);
                Score.scoreValue += 200;
            }else if (gameObject.tag.Equals("large"))
            {
                Destroy(gameObject);
                Score.scoreValue += 500;
            }
        }
    }
}
