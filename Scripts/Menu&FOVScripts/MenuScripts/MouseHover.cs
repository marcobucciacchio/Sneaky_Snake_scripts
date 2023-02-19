using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseHover : MonoBehaviour
{
    public Texture2D hoverTexture;
    public Texture2D defaultTexture;
    public Vector2 hotSpot = Vector2.zero;
    public CursorMode curMode = CursorMode.Auto;
    public bool visibility = true;

    void Start()
    {
        if (!visibility)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.SetCursor(defaultTexture, hotSpot, curMode);
        }
    }

    void OnMouseEnter()
    {
        Cursor.SetCursor(hoverTexture, hotSpot, curMode);
        
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(defaultTexture, hotSpot, curMode);

    }

}
