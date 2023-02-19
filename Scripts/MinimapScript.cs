using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapScript : MonoBehaviour
{
    public Transform player;
    float shadowDistance;

    private void LateUpdate()
    {
        Vector3 position = player.position;
        position.y = transform.position.y;
        transform.position = position;
    }


    //To not render shadows in the minimap (https://answers.unity.com/questions/1219726/make-shadows-disappear.html)
    //If we are to use this, we need to change the colors of the walls to make them visible

    /*
    private void OnPreRender()
    {
        shadowDistance = QualitySettings.shadowDistance;
        QualitySettings.shadowDistance = 0;
    }

    private void OnPostRender()
    {
        QualitySettings.shadowDistance = shadowDistance;
    }*/
}
