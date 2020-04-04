using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomPlaneSelectable : SelectableObj
{
    public override void onSelect()
    {
        base.onSelect();
    }

    public override void onUserClick()
    {
        base.onUserClick();
        RoomBound[] roomBounds = this.gameObject.transform.parent.GetComponentsInChildren<RoomBound>();
        if (roomBounds.Length != 4)
        {
            Debug.LogError("One of your rooms is missing bounds");
            return;
        }
        Vector3[] bounds = { roomBounds[0].transform.position, roomBounds[1].transform.position, roomBounds[2].transform.position, roomBounds[3].transform.position };

        float minX = int.MaxValue;
        float maxX = int.MinValue;
        float minZ = int.MaxValue;
        float maxZ = int.MinValue; 

        foreach (Vector3 f in bounds)
        {
            if (f.x < minX) minX = f.x;
            if (f.x > maxX) maxX = f.x;
            if (f.z < minZ) minZ = f.z;
            if (f.z > maxZ) maxZ = f.z; 
        }

        CameraMove.s.alignCamera(minX, maxX, minZ, maxZ);
        deactivateSelf();
        UIManager.s.showZoomedInUI(); 
    }

    private void deactivateSelf()
    {
        this.GetComponent<BoxCollider>().enabled = false; 
    }

    private void activateSelf()
    {
        this.GetComponent<BoxCollider>().enabled = true; 
    }

    private void Update()
    {
        CameraMove.onZoomOut += activateSelf;
        CameraMove.onZoomIn += deactivateSelf; 
    }
}
