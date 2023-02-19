using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    public float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 10.0f;
        
    }

    // Update is called once per frame
    void Update()
    {

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);

        }

        
       

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Enemy")
        {
            //add slowdown function for enemy and call
        }

    }
}