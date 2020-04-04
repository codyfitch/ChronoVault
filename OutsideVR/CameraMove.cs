using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static CameraMove s;
    private Vector3 cameraTarget;
    public float cameraSpeed;
    private List<float> basePosition;

    public delegate void OnZoomOut(); // icons subscribe to the delegate so they know when to change their size 
    public static event OnZoomOut onZoomOut; 

    public delegate void OnZoomIn();
    public static event OnZoomIn onZoomIn; 

    void Start()
    {
        s = this;
    }

    public void alignCamera(float minX, float maxX, float minZ, float maxZ)
    {
        float zDiff = Mathf.Abs(maxZ - minZ);
        float xDiff = Mathf.Abs(maxX - minX);

        float cameraDistance = 0.7f; // Constant factor
        Vector3 objectSizes = new Vector3(xDiff, 0, zDiff);
        float objectSize = Mathf.Max(objectSizes.x, objectSizes.y, objectSizes.z);
        float cameraView = 2.0f * Mathf.Tan(0.5f * Mathf.Deg2Rad * Camera.main.fieldOfView); // Visible height 1 meter in front
        float distance = cameraDistance * objectSize / cameraView; // Combined wanted distance from the object
        distance += 0.5f * objectSize; // Estimated offset from the center to the outside of the object
        cameraTarget = new Vector3((maxX + minX) / 2, 5, (maxZ + minZ) / 2) - distance * Camera.main.transform.forward;

        onZoomIn(); 
    }

    public void setBasePosition(float minX, float maxX, float minZ, float maxZ)
    {
        basePosition = new List<float>();
        basePosition.Add(minX);
        basePosition.Add(maxX);
        basePosition.Add(minZ);
        basePosition.Add(maxZ);
        returnToBasePosition(); 
    }

    public void returnToBasePosition()
    {
        if (basePosition != null)
        {
            alignCamera(basePosition[0], basePosition[1], basePosition[2], basePosition[3]);
            UIManager.s.hideZoomedInUI();
            onZoomOut(); 
        }
    }

    private void Update()
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, cameraTarget, Time.deltaTime * cameraSpeed); 
    }
}
