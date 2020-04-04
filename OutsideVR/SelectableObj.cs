using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableObj : MonoBehaviour
{
    private bool selected = false;
    private Renderer rend;

    private Color originalColor;
    public Color selectColor; 

    private void Start()
    {
        gameObject.tag = "selectable";
        rend = this.GetComponent<Renderer>();
        originalColor = rend.material.color; 
    }
    public virtual void onSelect()
    {
        selected = true;
        rend.material.color = selectColor;  
        // Cursor.setCursor 
    }

    public virtual void onUnselect()
    {
        selected = false;
        rend.material.color = originalColor; 
    }

    public virtual void onUserClick()
    {

    }

}
