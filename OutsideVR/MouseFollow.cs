using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollow : MonoBehaviour
{
    private float yValue = 0;

    private void Start()
    {
        this.transform.rotation = Quaternion.Euler(-90, 180, 0);
    }
    void Update()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 10f;
        this.transform.position = Camera.main.ScreenToWorldPoint(pos);
    }
}
