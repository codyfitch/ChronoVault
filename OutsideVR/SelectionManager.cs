using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    private SelectableObj selectedObj; 
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000.0f) && hit.transform.gameObject.tag == "selectable") // Mouse hover
        {                                                        // applies to any object with a SelectableObj script attached 
            selectedObj = hit.transform.gameObject.GetComponent<SelectableObj>(); 
            selectedObj.onSelect();
        } else if (selectedObj != null) // Mouse leave 
        {
            selectedObj.onUnselect(); 
            selectedObj = null; 
        }
        if (Input.GetMouseButtonDown(0) && selectedObj != null) // Click 
        {
            selectedObj.onUserClick(); 
        }
        if (Input.GetMouseButtonDown(1)) // Right Click 
        {
            CameraMove.s.returnToBasePosition(); 
        }
    }
}
