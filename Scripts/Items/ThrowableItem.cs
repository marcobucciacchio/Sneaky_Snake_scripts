using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableItem : MonoBehaviour
{
    public bool slowPotion, speedPotion;

    public float lifeTime;
    public GameObject puddle;
    public GameObject speed;

    Vector3 puddlePos;
    // Update is called once per frame
    void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
            if (slowPotion)
                Instantiate(puddle, transform.position, Quaternion.identity);

        }
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ground")
        {
            puddlePos = gameObject.transform.position;
            Destroy(gameObject);

        }

    }
}
