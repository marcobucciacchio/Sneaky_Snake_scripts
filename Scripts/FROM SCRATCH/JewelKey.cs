using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
public class JewelKey : MonoBehaviour
{
    [SerializeField]
    public Transform player;
    [SerializeField]
    bool Key1, Key2, Key3, Key4, Key5;

    public RawImage jewel1UI;
    public RawImage jewel2UI;
    public RawImage jewel3UI;
    public RawImage jewel4UI;
    public RawImage jewel5UI;


    Color currcolor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < 4f)
        {
            Destroy(gameObject);
            KeyScore.numKeys++;
            if (Key1)
            {
                currcolor = jewel1UI.color;
                currcolor.a = 1;
                jewel1UI.color = currcolor;
            }else if (Key2)
            {
                currcolor = jewel2UI.color;
                currcolor.a = 1;
                jewel2UI.color = currcolor;
            }else if (Key3)
            {
                currcolor = jewel3UI.color;
                currcolor.a = 1;
                jewel3UI.color = currcolor;
            }else if (Key4)
            {
                currcolor = jewel4UI.color;
                currcolor.a = 1;
                jewel4UI.color = currcolor;
                // jewel4UI.GetComponent<RawImage>().color.a.Equals(1);
            }
            else if (Key5)
            {
                currcolor = jewel4UI.color;
                currcolor.a = 1;
                jewel4UI.color = currcolor;
                // jewel4UI.GetComponent<RawImage>().color.a.Equals(1);
            }



        }
    }
}
